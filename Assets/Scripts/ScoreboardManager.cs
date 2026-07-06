using System;
using TMPro;
using UnityEngine;


public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        gameManager.OnScoreChanged += UpdateUI;
    }

    private void OnDisable()
    {
        gameManager.OnScoreChanged -= UpdateUI;
    }

    private void UpdateUI(int score)
    {
        scoreText.text = "Shots: " + score.ToString();
    }
}