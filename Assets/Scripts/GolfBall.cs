using System;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    public int modifier {get; set;}
    public bool canHopWalls {get; set;}
    public bool canPutt {get; set;}

    private void Awake()
    {
        modifier = 0;
        canHopWalls = false;
        canPutt = false;
    }
}