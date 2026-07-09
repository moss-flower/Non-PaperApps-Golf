using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelSelectorScreen : MonoBehaviour
{

    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private GameObject viewport;
    [SerializeField] private GameObject levelListHolderPrefab;
    
    [SerializeField] private LevelManager levelManager;
    
    private int selectedLevel = -1;
    private GameObject levelListParent;

    private void OnEnable()
    {
        if (levelManager == null)
        {
            print("[LevelSelectorScreen.cs] HEY, ASSIGN THE LEVEL MANAGER!");
            return;
        }

        var levels = levelManager.GetMapData();
        viewport.SetActive(true);
        if (levels == null)
        {
            return;
        }
        GenerateLevelList(levels);
    }

    private void OnDisable()
    {;
        Destroy(levelListParent);
        viewport.SetActive(false);
        selectedLevel = -1;
    }

    public void SelectLevel(int level)
    {
        selectedLevel = level;
    }

    public void OnConfirmedClicked()
    {
        if (selectedLevel == -1)
        {
            return;
        }
        levelManager.SelectLevel(selectedLevel);
    }

    private void GenerateLevelList(List<BoardInfo> boards)
    {
        
        if (levelListParent != null)
        {
            Destroy(levelListParent);
        }
        levelListParent = Instantiate(levelListHolderPrefab, viewport.transform);

        if (levelButtonPrefab == null)
        {
            throw new NullReferenceException("Missing: LevelButtonPrefab");
        }
        
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
        SelectLevel(level);
    }
    
    
}

