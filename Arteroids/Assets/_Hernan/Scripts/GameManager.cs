using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Menu,
    Start,
    InGame,
    Score
}

public class GameManager : MonoBehaviour
{
    public State state;
    public AsteroidsManager asteroidMgr;
    public PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Start;
    }

    // Update is called once per frame
    void Update()
    {
        switch ((int)state)
        {
            case 0:
                // menu
                break;
            case 1:
                asteroidMgr.CreateFirstAsteroids();
                state = State.InGame;
                break;
            case 2:
                // InGame
                break;
            case 3:
                // score
                break;
        }
    }
}
