using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private GameObject activeMenu;
    [SerializeField] private GameObject mainMenu;
    void Awake()
    {
        Open(mainMenu);
    }

    public void Open(GameObject menu)
    {
        if (activeMenu != null)
        {
            activeMenu.SetActive(false);
        }
        activeMenu = menu;
        activeMenu.SetActive(true);
    }

    public void Close()
    {
        activeMenu.SetActive(false);
    }
}
