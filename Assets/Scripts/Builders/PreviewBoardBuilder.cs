using Models;
using UnityEngine;

namespace Builders
{
    public class PreviewBoardBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject paintableTilePrefab;
        private GameObject boardRoot;
        private PaintableTile[,] previewTiles;
    
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