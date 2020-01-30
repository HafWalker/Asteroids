using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameManager gameMgr;
    private int scoreAmount;
    public Text scoreTxt;

    public int actualLives;
    public List<GameObject> lives;

    private void Start()
    {
        RestoreLives();
    }

    public void AddScore(int amount)
    {
        scoreAmount += amount;
        scoreTxt.text = scoreAmount.ToString();
    }

    public void RestoreLives()
    {
        actualLives = lives.Count-1;
        foreach (GameObject life in lives)
        {
            life.SetActive(true);
        }
    }

    public void RemoveLife()
    {
        lives[actualLives].SetActive(false);
        if (actualLives > 0)
        {
            actualLives--;
        }
        else
        {
            print("GameOver");
        }

    }
}
