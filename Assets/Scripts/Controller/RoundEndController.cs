using System;
using UnityEngine;

public class RoundEndController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private LevelManager levelManager;

    public void OnNextLevel()
    {
        if(levelManager == null){return;}
        levelManager.nextLevel();
    }

    public void OnMainMenu()
    {
        if(menuManager == null){return;}
        menuManager.Open(mainMenuUI);
    }
}
