using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

struct BoardInfo
{
    public String name;
    public String path;
}

public class LevelSelectorScreen : MonoBehaviour
{

    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private GameObject viewport;
    [SerializeField] private GameObject levelListHolderPrefab;
    
    private int selectedLevel = -1;
    private GameObject levelListParent;
    private List<BoardInfo> boards;
    
    [SerializeField] GameManager gameManager;


    private void Initialize()
    {
        if (gameManager == null)
        {
            print("[LevelSelectorScreen.cs] HEY, ASSIGN THE GAME MANAGER!");
        }
        selectedLevel = -1;
        viewport.SetActive(true);
        if (levelListParent != null)
        {
            Destroy(levelListParent);
        }
        levelListParent = Instantiate(levelListHolderPrefab, viewport.transform);
        GenerateLevelList();
    }
    
    private void Awake()
    {
        Initialize();
    }

    public void selectLevel(int level)
    {
        selectedLevel = level;
    }

    public void OnConfirmedClicked()
    {
        if (selectedLevel == -1)
        {
            return;
        }
        var levelName = boards[selectedLevel].path;
        if (levelName != "")
        {
            gameManager.Load(levelName);
        }
    }

    private List<BoardInfo> LoadMapMetaData()
    {
        List<BoardInfo> boardInfos = new List<BoardInfo>();

        TextAsset[] files = Resources.LoadAll<TextAsset>("Maps");
        foreach (var file in files)
        {
            BoardInfo info = JsonUtility.FromJson<BoardInfo>(file.text);
            info.path = file.name;
            boardInfos.Add(info);
        }
        return boardInfos;
    }

    private void GenerateLevelList()
    {
        if (levelListParent == null)
        {
            Initialize();
        }

        if (levelButtonPrefab == null)
        {
            throw new NullReferenceException("Missing: LevelButtonPrefab");
        }
        
        boards = LoadMapMetaData();
        if (boards.Count == 0)
        {
            return;
        }

        for (int i = 0; i < boards.Count; i++)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab, levelListParent.transform);
            LevelSelectButton button = levelButton.GetComponent<LevelSelectButton>();
            button.InitializeButton(boards[i].name, i, this);
        }
    }

    public void OnClick(int level)
    {
        selectLevel(level);
    }
    
    
}

