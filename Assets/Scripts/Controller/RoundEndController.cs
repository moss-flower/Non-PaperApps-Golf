using System;
using TMPro;
using UnityEngine;

public class RoundEndController : Menu
{
    [SerializeField] private Menu mainMenuUI;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text roundEndText;

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

    public override void Open()
    {
        base.Open();
        roundEndText.text = gameManager.gameState.getScore().ToString();
    }
}
