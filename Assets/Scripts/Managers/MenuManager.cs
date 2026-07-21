using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// A class for providing standard methods for menu transitions and overlays.
    /// </summary>
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

        /// <summary>
        /// Sets a new active primary menu. Closes current menu.
        /// </summary>
        /// <param name="menu">The <see cref="Menu"/> game object to be opened.</param>
        public void Open(Menu menu)
        {
            if (activeMenu != null)
            {
                activeMenu.Close();
            }
            activeMenu = menu;
            activeMenu.Open();
        }

        /// <summary>
        /// Creates an overly over the currently active primary menu.
        /// </summary>
        /// <param name="overlay">A <see cref="Menu"/> object.</param>
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

        /// <summary>
        /// Closes the topmost overlay.
        /// </summary>
        public void CloseOverlay()
        {
            if (overlays.Count > 0)
            {
                Menu overlay = overlays[^1];
                overlays.Remove(overlay);
                overlay.Close();
            }
        }

        /// <summary>
        /// Closes the current active primary menu.
        /// </summary>
        /// <remarks>unused.</remarks>
        public void Close()
        {
            activeMenu.Close();
        }
    }
}
