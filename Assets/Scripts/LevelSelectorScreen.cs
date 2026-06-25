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
    public event Action<int> onLevelConfirmed;


    private void Initialize()
    {
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
        if (onLevelConfirmed != null && selectedLevel != -1)
        {
            onLevelConfirmed?.Invoke(selectedLevel);
        }
        
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
        
        List<BoardInfo> boards = LoadMapMetaData();
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
        print(level);
    }
    
    
}

