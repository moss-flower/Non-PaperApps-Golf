using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class BoardFactory : MonoBehaviour
{
    public GameObject tilePrefab;
    public TileDefinition[] tileDefinitions;

    private Dictionary<string, TileDefinition> _dictionary;
    
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
                tileComponent.Initialize(tileDefinitions[random.Next(tileDefinitions.Length)], i, j);
                board.tiles[i, j] = tileComponent;
            }
        }
        return board;
    }

    public Board LoadBoardFromFile(string path, Transform parent)
    {
        
        TextAsset jsonFile = Resources.Load<TextAsset>(path);
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
            tileComponent.Initialize(ParseTileDefinition(data.type),  data.x, data.y);
            board.tiles[data.x,data.y] = tileComponent;
        }
        return board;
    }

    private void GenerateDictionary()
    {
        _dictionary = new Dictionary<string, TileDefinition>();
        foreach (TileDefinition tileDefinition in tileDefinitions)
        {
            print("Adding tile to dictionary: "  + tileDefinition.name);
            _dictionary.Add(tileDefinition.tileName, tileDefinition);
        }
    }
    
    // note: have the flag and tee stored as part of the serialization step so that when 
    // deserializing, we don't need to look for it.

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