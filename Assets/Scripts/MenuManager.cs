using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    //TODO: add "overlay" hierarchy list that allows for displaying several UI components over one another.
    
    private Menu activeMenu;
    private List<Menu> overlays = new List<Menu>();
    [SerializeField] private Menu mainMenu;
    [SerializeField] private GameObject displayRoot;
    private GameObject activeOverlay;
    void Awake()
    {
        Open(mainMenu);
    }

    public void Open(Menu menu)
    {
        if (activeMenu != null)
        {
            activeMenu.Close();
        }
        activeMenu = menu;
        activeMenu.Open();
    }

    public void AddOverlay(Menu overlay)
    {
        if (activeMenu != null && !activeMenu.IsCovered())
        {
            activeMenu.IsCovered();
        }
        overlays.Add(overlay);
        overlay.transform.SetParent(displayRoot.transform);
        overlay.transform.SetAsLastSibling();
        overlay.Open();
    }

    public void CloseOverlay()
    {
        if (overlays.Count > 0)
        {
            Menu overlay = overlays[^1];
            overlays.Remove(overlay);
            overlay.Close();
        }
    }

    public void Close()
    {
        activeMenu.Close();
    }
}
