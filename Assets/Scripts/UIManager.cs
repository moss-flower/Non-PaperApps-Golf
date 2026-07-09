using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject gameUI;

    
    //note, this is where we could technically use an interface and swappable components to handle
    //updates and transitioning between scenes. (Eg, a universal update call so that the UI manager didn't have to care)
    //for the time being we'll do it manually
    
    //we can use this to trigger the round over screen on round win
    
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