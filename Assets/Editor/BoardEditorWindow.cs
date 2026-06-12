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
        
        private GameObject boardRoot;
        private PaintableTile[,] previewTiles;

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
            if (boardRoot != null)
            {
                ClearBoard();
            }
            
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
                    previewTiles = new PaintableTile[width, height];
                }
            }
        }

        private void ClearBoard()
        {
            if (boardRoot != null)
            {
                DestroyImmediate(boardRoot);
            }

            previewTiles = null;
        }

        private void SaveBoardToJSON()
        {
            
        }
    }
}