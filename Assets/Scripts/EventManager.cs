using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public event Action<int,int> OnTileClick;

    public void OnClickEvent(int x, int y)
    {
        if (OnTileClick != null)
        {
            OnTileClick(x, y);
        }
    }
}