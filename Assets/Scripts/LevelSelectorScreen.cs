using System;
using UnityEngine;

public class LevelSelectorScreen : MonoBehaviour
{
    // need to build the actual UI fabricator for the level selector
    private int selectedLevel = -1;
    public event Action<int> onLevelConfirmed; 
    
    public void selectLevel(int level)
    {
        selectedLevel = level;
    }

    public void OnConfirmedClicked()
    {
        if (onLevelConfirmed != null && selectedLevel != -1)
        {
            onLevelConfirmed?.Invoke(selectedLevel);
        }
        
    }
}