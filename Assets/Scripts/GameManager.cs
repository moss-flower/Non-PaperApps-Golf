using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board { get; private set; }
    [SerializeField] private BoardFactory boardFactory;
    private DiceRoller diceRoller;
    [SerializeField] private GameObject golfBallPrefab;
    private GolfBall golfBall;
    

    private void Awake()
    {
        diceRoller = new DiceRoller();
    }

    private void Start()
    {
        board = boardFactory.CreateBoard(3, 5);
        GameObject ball = Instantiate(golfBallPrefab, board.tiles[0, 0].transform.position, new Quaternion());
        golfBall = ball.GetComponent<GolfBall>();
    }
}