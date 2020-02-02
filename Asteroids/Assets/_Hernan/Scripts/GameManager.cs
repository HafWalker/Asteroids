using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    GameStart,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    public AsteroidsManager asteroidMgr;
    public PlayerStatus playerStatus;

    public GameObject canvasMenu;
    public GameObject canvasGame;
    public GameObject canvasScore;

    public Text highScoreTxt;
    private int actualScore;
    private int highScore; // Seria posible guardar este valor en PlayerPrefs para persistirlo entre ejecuciones

    void Start()
    {
        highScoreTxt.text = "0";
        ChangeGameState(GameState.Menu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == GameState.Menu)
            {
                ChangeGameState(GameState.GameStart);
            }
            else if(gameState == GameState.GameOver) 
            {
                ChangeGameState(GameState.Menu);
            }
        }

        if (gameState == GameState.GameStart)
        {
            actualScore = playerStatus.GetScore();

            if (actualScore > highScore)
            {
                highScore = actualScore;
                highScoreTxt.text = highScore.ToString();
            }

            if (playerStatus.isDead)
            {
                ChangeGameState(GameState.GameOver);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ChangeGameState(GameState newState)
    {
        gameState = newState;

        switch ((int)newState)
        {
            case 0:
                StopAllCoroutines();
                canvasMenu.SetActive(true);
                canvasScore.SetActive(false);
                canvasGame.SetActive(false);
                break;
            case 1:
                canvasMenu.SetActive(false);
                canvasGame.SetActive(true);
                playerStatus.Initialize();
                ResetWorld();
                break;
            case 2:
                canvasScore.SetActive(true);
                canvasGame.SetActive(false);
                break;
        }
    }

    public void ResetWorld()
    {
        asteroidMgr.ResetAsteroids();
        playerStatus.Respawn();
        playerStatus.GetComponent<ShipController>().ResetPos(); // castear
    }
}
