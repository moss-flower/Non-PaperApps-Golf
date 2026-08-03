using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    
    /// <summary>
    /// Class used to represent a board in the level selection screen.
    /// </summary>
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField] TMP_Text levelNameText;
        [SerializeField] TMP_Text levelDescriptionText;
        private int index;
        private LevelSelectorMenu levelSelectorMenu;
        private Button button;
        private Image backgroundImage;
        private Color defaultColor;
    
        /// <summary>
        /// Effectively a constructor function. Populates the button after the prefab object has been initialized.
        /// </summary>
        /// <param name="name">Level name</param>
        /// <param name="index">Which index the level is in the Level managers list.</param>
        /// <param name="menu">A reference to the menu, for some reason.</param>
        public void InitializeButton(string name, int index, LevelSelectorMenu menu)
        {
            levelNameText.text = name;
            this.index = index;
            levelDescriptionText.text = index.ToString();
            levelSelectorMenu = menu;
            button = GetComponent<Button>();
            backgroundImage = GetComponent<Image>();
            defaultColor = backgroundImage.color;
        }

        public void OnClick()
        {
            levelSelectorMenu.OnClick(index, this);
        }

        public void setActive()
        {
            backgroundImage.color = button.colors.selectedColor;
        }

        public void setInactive()
        {
            backgroundImage.color = defaultColor;
        }
        
        
    }
}