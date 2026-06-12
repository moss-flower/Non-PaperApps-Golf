using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class BoardEditorWindow : EditorWindow
    {
        [MenuItem("Tools/Board Editor")]
        private static void ShowWindow()
        {
            var window = GetWindow<BoardEditorWindow>();
            window.titleContent = new GUIContent("Board Editor");
            window.Show();
        }

        private string boardName;
        private int width, height;
        private GameObject paintableTilePrefab;
        private TileDefinition selectedTileDefinition;
        private float brushRadius;
        
        private GameObject boardRoot;
        private PaintableTile[,] previewTiles;
        private bool isPreviewing => boardRoot != null;
        
        private void OnEnable() {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable() {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
        

        private void OnGUI()
        {
            boardName = EditorGUILayout.TextField("Board Name", boardName);
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);
            paintableTilePrefab = EditorGUILayout.ObjectField(paintableTilePrefab, typeof(GameObject), false) as GameObject;
            EditorGUILayout.Space();

            if (GUILayout.Button("Create Board"))
            {
                GeneratePreview();
            }
            if (GUILayout.Button("Load Board"))
            {
                LoadBoard();
            }
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Paint With: ");
            selectedTileDefinition = EditorGUILayout.ObjectField(selectedTileDefinition, typeof(TileDefinition), false) as TileDefinition;
            brushRadius = EditorGUILayout.FloatField("Brush Radius", brushRadius);
            if (GUILayout.Button("Save Board"))
            {
                SaveBoardToJSON();
            }

            if (GUILayout.Button("Clear Board"))
            {
                ClearBoard();
            }

            GUI.enabled = true;
        }

        private void LoadBoard()
        {
            
        }
        
        private void GeneratePreview()
        {
            if (boardRoot != null) ClearBoard();
            
            boardRoot = new GameObject("Board");
            previewTiles = new PaintableTile[width, height];
            if (paintableTilePrefab == null)
            {
                ClearBoard();
                return;
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject tileInstance = Instantiate(paintableTilePrefab, new Vector3(i*0.5f, j*0.5f, 0), Quaternion.identity, boardRoot.transform);
                    PaintableTile tile = tileInstance.GetComponent<PaintableTile>();
                    previewTiles[i, j] = tile;
                }
            }
        }

        private void OnSceneGUI(SceneView view)
        {
            if(!isPreviewing || selectedTileDefinition == null) return;
            
            Event detectedEvent = Event.current;

            if ((detectedEvent.type == EventType.MouseDown || detectedEvent.type == EventType.MouseDrag) &&
                detectedEvent.button == 0)
            {
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
                
                Ray ray = HandleUtility.GUIPointToWorldRay(detectedEvent.mousePosition);
                float distToPlane = (0f - ray.origin.z) / ray.direction.z;
                Vector3 worldPoint3D = ray.origin + ray.direction * distToPlane;
                Vector2 origin = new Vector2(worldPoint3D.x, worldPoint3D.y);
                
                Handles.color = Color.red;
                Handles.DrawSolidDisc(origin, Vector3.back, brushRadius);
                Handles.color = Color.yellow;
                Handles.DrawWireDisc(origin, Vector3.back, brushRadius);
                
                
                
                Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, brushRadius*0.1f);

                foreach (Collider2D collider in colliders)
                {
                    PaintableTile tile =  collider.GetComponent<PaintableTile>();
                    if (tile != null)
                    {
                        Undo.RecordObject(tile, "Paint Tile");
                        tile.ApplyDefinition(selectedTileDefinition);
                    }
                }
                detectedEvent.Use();
            }
            
            /*if (detectedEvent.type == EventType.MouseDown && detectedEvent.button == 0)
            {
                
                GameObject picked = HandleUtility.PickGameObject(detectedEvent.mousePosition, true);
                if (picked != null)
                {
                    PaintableTile tile = picked.GetComponent<PaintableTile>();
                    if (tile != null)
                    {
                        Undo.RecordObject(paintableTilePrefab, "Paint Tile");
                        tile.ApplyDefinition(selectedTileDefinition);
                    }
                }
            }*/
            SceneView.RepaintAll();
        }

        private void ClearBoard()
        {
            if (boardRoot != null) DestroyImmediate(boardRoot);
            previewTiles = null;
        }

        private void SaveBoardToJSON()
        {
            var data = new BoardData
            {
                name = boardName,
                width = width,
                height = height,
                tiles = new List<TileData>()
            };

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tile = previewTiles[x, y];
                    // Note for later: Currently saving EVERY tile.
                    // Can actually just save every tile that's not a Rough tile
                    // But this is easier for now.
                    if (tile.tileDefinition.isStartingTile)
                    {
                        data.startTileLocation = (x, y);
                    }

                    if (tile.tileDefinition.isWinningTile)
                    {
                        data.winTileLocation = (x, y);
                    }
                    
                    data.tiles.Add(new TileData
                    {
                        x = x,
                        y = y,
                        type = tile.tileDefinition.tileName
                    });
                }
            }
            
            string path = EditorUtility.SaveFilePanel("Save Board", "Assets/Boards", "Board", "json");
            if (string.IsNullOrEmpty(path)) return;
            File.WriteAllText(path, JsonUtility.ToJson(data, true));
            AssetDatabase.Refresh();
        }
    }
}