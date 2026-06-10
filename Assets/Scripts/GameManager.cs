using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board { get; private set; }
    [SerializeField] private BoardFactory boardFactory;

    private void Awake()
    {
        
    }

    private void Start()
    {
        board = boardFactory.CreateBoard(3, 5);
    }
}