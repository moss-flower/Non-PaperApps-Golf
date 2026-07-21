using System;
using System.Collections.Generic;
using Controller;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;
using Models;

namespace Factories
{
    /// <summary>
    /// Class for instantiating the visual representation of the <see cref="Board"/>
    /// </summary>
    public class BoardFactory : MonoBehaviour
{
    public GameObject tilePrefab;
    public TileDefinition[] tileDefinitions;

    private Dictionary<string, TileDefinition> _dictionary;
    [FormerlySerializedAs("eventManager")] [SerializeField] private GameEventHandler gameEventHandler;
    
    /// <summary>
    /// Method for instantiating a completely new board.
    /// </summary>
    /// <remarks>Currently unused.</remarks>
    /// <param name="width">The width of the board in tiles.</param>
    /// <param name="height">The height of the board in tiles.</param>
    /// <param name="parent">The object to which the board should be parented.</param>
    /// <returns></returns>
    public Board CreateBoard(int width, int height, Transform parent)
    {
        //somewhere in here we'll have to take some sort of input so we can 
        //automatically generate the board with the correct definitions
        //probably json or something.
        Board board = new Board(width, height);
        int size = tileDefinitions.Length;
        Random random = new Random();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i*0.5f, j*0.5f, 0), Quaternion.identity, parent);
                Tile tileComponent = tile.GetComponent<Tile>();
                tileComponent.Initialize(tileDefinitions[random.Next(tileDefinitions.Length)], i, j, gameEventHandler);
                board.tiles[i, j] = tileComponent;
            }
        }
        return board;
    }

    /// <summary>
    /// A method for loading a gameboard from a file. Called by the Game manager during the
    /// "load" method.
    /// </summary>
    /// <param name="path">The path to the file in the resources folder</param>
    /// <param name="parent">The transform of the object to which the board should be parented.</param>
    /// <returns>Returns a <see cref="Board"/> object</returns>
    public Board LoadBoardFromFile(string path, Transform parent)
    {
        // Appending "Maps/" to target the maps folder of the Resources folder specifically.
        var path_complete = "Maps/" + path;
        
        //Map data is loaded into memory as a Text Asset
        TextAsset jsonFile = Resources.Load<TextAsset>(path_complete);
        if (jsonFile == null)
        {
            Debug.LogError($"File {path} does not exist");
            return null;
        }
        if (_dictionary == null || _dictionary.Count == 0)
        {
            GenerateDictionary();
        }
        BoardData boardData = JsonUtility.FromJson<BoardData>(jsonFile.text);
        if (boardData == null)
        {
            Debug.LogError($"File {path} is empty");
            return null;
        }
        
        Board  board = new Board(boardData.width, boardData.height);

        foreach (TileData data in boardData.tiles)
        {
            GameObject tile = Instantiate(tilePrefab, new Vector3(data.x*0.5f, data.y*0.5f, 0), Quaternion.identity, parent);
            Tile tileComponent = tile.GetComponent<Tile>();
            tileComponent.Initialize(ParseTileDefinition(data.type),  data.x, data.y, gameEventHandler);
            board.tiles[data.x,data.y] = tileComponent;
        }
        board.startTileLocation = boardData.startTileLocation;
        board.winTileLocation = boardData.winTileLocation;
        return board;
    }

    /// <summary>
    /// Manually generate a dictionary for tile construction purposes.
    /// </summary>
    private void GenerateDictionary()
    {
        _dictionary = new Dictionary<string, TileDefinition>();
        foreach (TileDefinition tileDefinition in tileDefinitions)
        {
            print("Adding tile to dictionary: "  + tileDefinition.name);
            _dictionary.Add(tileDefinition.tileName, tileDefinition);
        }
    }

    /// <summary>
    /// Used during an earlier conception of the board, currently unused. 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    private Vector2Int ConvertBoardDataInfoToVector2Int((int x, int y) info)
    {
        return new Vector2Int(info.Item1, info.Item2);
    }
    
    // note: have the flag and tee stored as part of the serialization step so that when 
    // deserializing, we don't need to look for it.

    /// <summary>
    /// Simple string matching for parsing Tile Definitions from the dictionary.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private TileDefinition ParseTileDefinition(string input)
    {
        switch (input)
        {
            case "Rough": return _dictionary["Rough"];
            case "Sand": return _dictionary["Sand"];
            case "Fairway": return _dictionary["Fairway"];
            case "Tree": return _dictionary["Tree"];
            case "Water": return _dictionary["Water"];
            case "Tee": return _dictionary["Tee"];
            case "Flag": return _dictionary["Flag"];
            default: return _dictionary["Rough"];
        }
    }
}
}
