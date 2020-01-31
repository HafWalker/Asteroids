using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    Menu,
    Start,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public State state;
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
        state = State.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        switch ((int)state)
        {
            case 0:
                // menu
                canvasMenu.SetActive(true);
                canvasScore.SetActive(false);
                canvasGame.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }

                break;
            case 1:
                canvasMenu.SetActive(false);
                canvasGame.SetActive(true);

                print("START");

                playerStatus.RestoreStatus();
                ResetWorld();

                state = State.InGame;
                break;
            case 2:

                actualScore = playerStatus.GetScore();

                if (actualScore > highScore)
                {
                    highScore = actualScore;
                    highScoreTxt.text = highScore.ToString();
                }

                // InGame
                break;
            case 3:
                canvasScore.SetActive(true);
                canvasGame.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StopAllCoroutines();
                    state = State.Menu;
                }

                //StartCoroutine(TimeToGoMenu());
                // score
                break;
        }
    }

    public void StartGame()
    {
        state = State.Start;
    }

    public void ResetWorld()
    {
        asteroidMgr.ResetAsteroids();
        playerStatus.Respawn();
        playerStatus.GetComponent<ShipController>().ResetPos(); // castear
    }

    public void SetGameOver()
    {
        print("Game Over");
        state = State.GameOver;
    }

    public IEnumerator TimeToGoMenu()
    {
        yield return new WaitForSeconds(5);
        state = State.Menu;
    }
}
