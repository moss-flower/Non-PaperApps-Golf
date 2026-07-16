using System;
using Helpers;
using TMPro;
using UnityEngine;

public class RoundEndController : Menu
{
    [SerializeField] private Menu mainMenuUI;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text roundEndText;
    [SerializeField] private TMP_Text par_num_Text;
    [SerializeField] private TMP_Text calculated_par_result_Text;
    [SerializeField] private GolfScorer scorer;

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
        if (scorer == null)
        {
            scorer = new GolfScorer();
        }
        base.Open();
        int finalScore = gameManager.gameState.getScore();
        int coursePar = levelManager.GetCurrentLevel().par;
        
        roundEndText.text = finalScore.ToString();
        par_num_Text.text = coursePar.ToString();
        if (par_num_Text.text == null)
        {
            par_num_Text.text = "3";
        }
        calculated_par_result_Text.text = scorer.Score(finalScore - coursePar);
    }
}
