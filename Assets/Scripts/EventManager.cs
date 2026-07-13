using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public void OnClickEvent(int x, int y)
    {
        gameManager.MoveEvent(x,y);
    }

    public void OnClickRole()
    {
        gameManager.HandleRoll();
    }
}