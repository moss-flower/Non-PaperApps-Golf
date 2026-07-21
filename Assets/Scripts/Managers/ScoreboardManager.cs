using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreboardManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI rollText;
        [SerializeField] private TextMeshProUGUI mulliganText;
        [SerializeField] private TextMeshProUGUI parText;
        [SerializeField] private TextMeshProUGUI levelName;

        private void OnEnable()
        {
            gameManager.OnScoreChanged += UpdateScoreUI;
            gameManager.OnRoll += UpdateRollUI;
            UpdateParText(levelManager.GetCurrentLevel().par);
            Initialize();
        
        }

        private void OnDisable()
        {
            gameManager.OnScoreChanged -= UpdateScoreUI;
            gameManager.OnRoll -= UpdateRollUI;
        }

        private void Initialize()
        {
            rollText.text = "Roll: ";
            mulliganText.text = "Mulligan:  3/3";
            levelName.text = levelManager.GetCurrentLevel().name;
        }

        private void UpdateScoreUI(int score)
        {
            scoreText.text = "Shots: " + score.ToString();
        
        }

        private void UpdateRollUI(int roll, int remainingMulligans)
        {
            rollText.text = "Roll: " + roll.ToString();
            mulliganText.text = "Mulligan:  " + remainingMulligans.ToString() + "/3";
        }

        private void UpdateParText(int par)
        {
            parText.text = "Par: " + par.ToString();
        }
    }
}