using TMPro;
using UnityEngine;


public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] TMP_Text levelNameText;
    [SerializeField] TMP_Text levelDescriptionText;
    private int index;
    private LevelSelectorMenu levelSelectorMenu;
    
    public void InitializeButton(string name, int index, LevelSelectorMenu menu)
    {
        levelNameText.text = name;
        this.index = index;
        levelDescriptionText.text = index.ToString();
        levelSelectorMenu = menu;
    }

    public void OnClick()
    {
        levelSelectorMenu.OnClick(index);
    }
}