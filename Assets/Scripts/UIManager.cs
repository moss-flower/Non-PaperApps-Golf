using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject gameUI;

    void OnEnable()
    {
        GameManager.OnGameStart += LoadGameUI;
    }
    void OnDisable()
    {
        GameManager.OnGameStart -= LoadGameUI;
    }

    void LoadGameUI()
    {
        menuManager.Open(gameUI);
    }
}