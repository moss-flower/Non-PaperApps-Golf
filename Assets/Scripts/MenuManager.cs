using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    //TODO: add "overlay" hierarchy list that allows for displaying several UI components over one another.
    
    private GameObject activeMenu;
    private List<GameObject> overlays = new List<GameObject>();
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject displayRoot;
    private GameObject activeOverlay;
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

    public void AddOverlay(GameObject overlay)
    {
        overlays.Add(overlay);
        overlay.transform.SetParent(displayRoot.transform);
        overlay.transform.SetAsLastSibling();
        overlay.SetActive(true);
    }

    public void CloseOverlay()
    {
        if (overlays.Count > 0)
        {
            GameObject overlay = overlays[^1];
            overlays.Remove(overlay);
            overlay.SetActive(false);
        }
    }

    public void Close()
    {
        activeMenu.SetActive(false);
    }
}
