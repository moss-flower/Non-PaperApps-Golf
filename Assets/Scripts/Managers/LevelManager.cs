using System;
using System.Collections.Generic;
using UnityEngine;

public struct BoardInfo
{
    public string name;
    public string path;
    public int par;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    
    private BoardInfo currentLevel;
    private int currentLevelIndex;
    private List<BoardInfo> levels;
    
    //alright so I'm thinking we get an explicit reference to our UI manager, which has events that are activated by buttons
    // when those events are activated, we trigger the scene transition logic here
    // thus prompting it to load the next level
    // theoretically, we could also provide that information in some other way
    // eg, on the round end event, we could call a function and display information about the next level
    // such as the level name, if it's been beaten before, etc.
    
    //now that I think about it, I should probably introduce a database to the project, for tracking score.
    

    private void Start()
    {
        if (gameManager == null)
        {
            print("[LevelManager] No game manager found");
            return;
        }
        UpdateLevels();
    }

    //method should be called sparingly, as it calls LoadMapMetaData, which will reset and rebuild the list.
    //This shouldn't be a problem, but just be careful if there's any dynamic loading of new maps, it could mess with the level index
    //by reordering the list.
    
    private void UpdateLevels()
    {
        currentLevelIndex = 0; // figured we should reset for safety reasons
        levels = LoadMapMetaData();
        if (levels.Count <= 0)
        {
            print("No levels found");
            return;
        }
        else //unnecessary just wanted to formalize the if else relationship
        {
            currentLevel = levels[currentLevelIndex];
        }
    }

    public List<BoardInfo> GetMapData()
    {
        if (levels == null)
        {
            return null;
        }
        return levels;
    }

    private List<BoardInfo> LoadMapMetaData()
    {
        List<BoardInfo> boards = new List<BoardInfo>();

        TextAsset[] files = Resources.LoadAll<TextAsset>("Maps");
        foreach (var file in files)
        {
            BoardInfo info = JsonUtility.FromJson<BoardInfo>(file.text);
            info.path = file.name;
            boards.Add(info);
        }
        return boards;
    }

    public void SelectLevel(int level)
    {
        currentLevelIndex = level;
        currentLevel = levels[level];
        var levelPath = levels[currentLevelIndex].path;
        if (levelPath != "")
        {
            gameManager.Load(levelPath);
        }
    }
    
    public void nextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Count)
        {
            //should probably figure out what to do with this
            return;
        }
        currentLevel = levels[currentLevelIndex];
        gameManager.Load(currentLevel.path);
    }

    public BoardInfo GetCurrentLevel()
    {
        return currentLevel;
    }
    
}
