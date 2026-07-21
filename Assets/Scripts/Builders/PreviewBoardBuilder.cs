using Models;
using UnityEngine;

namespace Builders
{
    /// <summary>
    /// A class used to create a preview board for the Level Creation editor tool.
    /// </summary>
    /// <remarks>Not used in the final game.</remarks>
    public class PreviewBoardBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject paintableTilePrefab;
        private GameObject boardRoot;
        private PaintableTile[,] previewTiles;
        
        
        /// <summary>
        /// Method for generating a new board.
        /// </summary>
        /// <param name="x">An integer representing the width of the board in tiles.</param>
        /// <param name="y">An integer representing the height of the board in tiles.</param>
        public void generateBoard(int x, int y)
        {
            boardRoot = new GameObject("Board");
            if (paintableTilePrefab == null)
            {
                clearBoard();
                return;
            }
        
            boardRoot = new GameObject("Board");
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    GameObject tileInstance = Instantiate(paintableTilePrefab, new Vector3(i*0.5f, j*0.5f, 0), Quaternion.identity, boardRoot.transform);
                    PaintableTile tile = tileInstance.GetComponent<PaintableTile>();
                    previewTiles = new PaintableTile[x, y];
                }
            }
        }

        /// <summary>
        /// A method for destroying the board object when it is ready to be discarded.
        /// </summary>
        /// <remarks>Should be called BEFORE changing scenes.</remarks>
        public void clearBoard()
        {
            boardRoot.SetActive(false);
            foreach (PaintableTile tile in previewTiles)
            {
                Destroy(tile.gameObject);
            }
            Destroy(boardRoot.gameObject);
        }
    
    }
}