using UnityEngine;

public class RoundEndController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private LevelManager levelManager;

    public void OnNextLevel()
    {
        levelManager.nextLevel();
    }

    public void OnMainMenu()
    {
        menuManager.Open(mainMenuUI);
    }
}
