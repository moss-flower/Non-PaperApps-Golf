using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Models
{
    /// <summary>
    /// A standard C# class that represents the data portion of a Board Object.
    /// Contains the information necessary to model a board for use in the game.
    /// </summary>
    public class Board
    {
        public readonly Tile[,] tiles;
        public List<Tile> highLightedTiles = new List<Tile>();
        private readonly Vector2Int dimensions;
        public Vector2Int startTileLocation;
        public Vector2Int winTileLocation;


        /// <summary>
        /// A constructor method for creating a new board.
        /// </summary>
        /// <param name="width">An integer representing the width of the board in number of tiles.</param>
        /// <param name="height">An integer representing the height of the board in tiles.</param>
        public Board(int width, int height)
        {
            tiles = new Tile[width, height];
            dimensions = new Vector2Int(width, height);
        }

        /// <summary>
        /// A method for retrieving the information about a tile from the board.
        /// </summary>
        /// <param name="coordinates">A <see cref="Vector2Int"/> representing the x and y coordinates (position)
        /// of a tile on the board.</param>
        /// <returns>Returns a <see cref="Tile"/> object.</returns>
        public Tile GetTile(Vector2Int coordinates)
        {
            return tiles[coordinates.x, coordinates.y];
        }

        /// <summary>
        /// A method used by the <see cref="GameManager"/> for checking whether a tile exists.
        /// Used to determine the validity of a possible move.
        /// </summary>
        /// <param name="coordinates">A <see cref="Vector2Int"/> representing the X and Y coordinates of a tile
        /// in the game board.</param>
        /// <returns>Returns true if the tile both exists (is within the bounds of the board) and
        /// can be targeted (a characteristic of the tile).</returns>
        public bool IsInBoundsAndIsTartetable(Vector2Int coordinates)
        {
            if (coordinates.x > dimensions.x - 1 || coordinates.x < 0) return false;
            if (coordinates.y > dimensions.y - 1 || coordinates.y < 0) return false;

            if (!tiles[coordinates.x, coordinates.y].tileDefinition.isTargetable) return false;

            return true;
        }



    }
}