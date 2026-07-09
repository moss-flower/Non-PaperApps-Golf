using System;
using TMPro;
using UnityEngine;


public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI rollText;
    [SerializeField] private GameObject gameWonBanner;

    private void OnEnable()
    {
        gameManager.OnScoreChanged += UpdateScoreUI;
        gameManager.OnRoll += UpdateRollUI;
        gameManager.OnRoundEnd += OnGameWon;
    }

    private void OnDisable()
    {
        gameManager.OnScoreChanged -= UpdateScoreUI;
        gameManager.OnRoll -= UpdateRollUI;
        gameManager.OnRoundEnd -= OnGameWon;
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.text = "Shots: " + score.ToString();
        
    }

    private void UpdateRollUI(int roll)
    {
        rollText.text = "Roll: " + roll.ToString();
    }

    private void OnGameWon()
    {
        gameWonBanner.SetActive(true);
    }
}