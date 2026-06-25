using TMPro;
using UnityEngine;


public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] TMP_Text levelNameText;
    [SerializeField] TMP_Text levelDescriptionText;
    private int index;
    private LevelSelectorScreen levelSelectorScreen;
    
    public void InitializeButton(string name, int index, LevelSelectorScreen screen)
    {
        levelNameText.text = name;
        this.index = index;
        levelDescriptionText.text = index.ToString();
        levelSelectorScreen = screen;
    }

    public void OnClick()
    {
        levelSelectorScreen.OnClick(index);
    }
}