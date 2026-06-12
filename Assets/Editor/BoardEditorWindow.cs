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
        private TileDefinition selectedTileDefinition;

        private void OnGUI()
        {
            boardName = EditorGUILayout.TextField("Board Name", boardName);
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);

            if (GUILayout.Button("Create Board"))
            {
                GenerateBoard();
            }
            if (GUILayout.Button("Load Board"))
            {
                LoadBoard();
            }
            
            EditorGUILayout.LabelField("Paint With: ");
            //selectedTileDefinition = EditorGUILayout.ObjectField(selectedTileDefinition, typeof(TileDefinition), false) as TileDefinition;

            if (GUILayout.Button("Save Board"))
            {
                SaveBoardToJSON();
            }

            if (GUILayout.Button("Clear Board"))
            {
                ClearBoard();
            }

            
        }

        private void LoadBoard()
        {
            
        }

        private void GenerateBoard()
        {
            
        }

        private void ClearBoard()
        {
            
        }

        private void SaveBoardToJSON()
        {
            
        }
    }
}