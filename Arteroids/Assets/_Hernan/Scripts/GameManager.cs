using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    Init,
    Game,
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
    private int highScore;

    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        highScoreTxt.text = "0";
        gameState = GameState.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        switch ((int)gameState)
        {
            case 0:
                // Sate Menu

                canvasMenu.SetActive(true);
                canvasScore.SetActive(false);
                canvasGame.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameState = GameState.Init;
                }

                break;

            case 1:
                // Sate Init

                canvasMenu.SetActive(false);
                canvasGame.SetActive(true);

                playerStatus.Initialize();
                ResetWorld();

                gameState = GameState.Game;

                break;

            case 2:
                // Sate Game

                actualScore = playerStatus.GetScore();

                if (actualScore > highScore)
                {
                    highScore = actualScore;
                    highScoreTxt.text = highScore.ToString();
                }

                if (playerStatus.isDead)
                {
                    gameState = GameState.GameOver;
                }

                break;

            case 3:
                // Sate GameOver

                canvasScore.SetActive(true);
                canvasGame.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopAllCoroutines();
                    gameState = GameState.Menu;
                }

                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ResetWorld()
    {
        asteroidMgr.ResetAsteroids();
        playerStatus.Respawn();
        playerStatus.GetComponent<ShipController>().ResetPos(); // castear
    }

}
