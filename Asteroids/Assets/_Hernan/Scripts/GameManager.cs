using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GAMESTATE {
    MENU,
    START,
    ENDGAME
}

public class GameManager : MonoBehaviour {

    public GAMESTATE gameState;
    public AsteroidsManager asteroidManager;
    public PlayerStatus playerStatus;
    public GameUI gameUI;

    public GameObject canvasMenu;
    public GameObject canvasGame;
    public GameObject canvasScore;
    public GameObject gameContainer;

    private int m_actualScore;
    private int m_highScore;

    void Start() {
        ChangeGameState(GAMESTATE.MENU);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (gameState == GAMESTATE.MENU) {
                ChangeGameState(GAMESTATE.START);
            } else if(gameState == GAMESTATE.ENDGAME) {
                ChangeGameState(GAMESTATE.MENU);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void ChangeGameState(GAMESTATE newState) {
        gameState = newState;

        switch ((int)newState) {
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

    public void ResetStage() {
        asteroidManager.ResetAsteroids();
        playerStatus.Respawn();
        playerStatus.GetComponent<ShipController>().ResetPos();
    }

    public void SetGameOver() {
        ChangeGameState(GAMESTATE.ENDGAME);
    }

    public void CheckHighScore() {
        m_actualScore = playerStatus.GetScore();

        if (m_actualScore > m_highScore) {
            m_highScore = m_actualScore;
            gameUI.SetHighScore(m_actualScore);
        }
    }

    private void OnEnable() {
        PlayerStatus.OnPlayerDeath += SetGameOver;
    }

    private void OnDisable() {
        PlayerStatus.OnPlayerDeath -= SetGameOver;
    }
}