using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    GameStart,
    GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    protected GameState gameState;

    public AsteroidsManager asteroidMgr;
    public PlayerStatus playerStatus;
    public GameUI gameUI;

    [SerializeField]
    protected GameObject canvasMenu;

    [SerializeField]
    protected GameObject canvasGame;

    [SerializeField]
    protected GameObject gameContainer;

    [SerializeField]
    protected GameObject canvasScore;

    private int actualScore;
    private int highScore;

    void Start()
    {
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
                gameContainer.SetActive(false);
                break;
            case 1:
                canvasMenu.SetActive(false);
                canvasGame.SetActive(true);
                gameContainer.SetActive(true);
                playerStatus.Initialize();
                ResetStage();
                break;
            case 2:
                canvasScore.SetActive(true);
                canvasGame.SetActive(false);
                gameContainer.SetActive(false);
                break;
        }
    }

    public void ResetStage()
    {
        asteroidMgr.ResetAsteroids();
        playerStatus.Respawn();
        playerStatus.GetComponent<ShipController>().ResetPos(); // castear
    }

    public void SetGameOver()
    {
        ChangeGameState(GameState.GameOver);
    }

    public void CheckHighScore()
    {
        actualScore = playerStatus.GetScore();

        if (actualScore > highScore)
        {
            highScore = actualScore;
            gameUI.SetHighScore(actualScore);
        }
    }

    private void OnEnable()
    {
        PlayerStatus.OnPlayerDeath += SetGameOver;
    }

    private void OnDisable()
    {
        PlayerStatus.OnPlayerDeath -= SetGameOver;
    }
}
