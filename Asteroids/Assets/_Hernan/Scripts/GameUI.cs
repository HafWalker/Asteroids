using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    protected Text scoreTxt;

    [SerializeField]
    protected List<GameObject> livesGameObjects;

    [SerializeField]
    protected Text highScoreTxt;

    public void RestLife(int index)
    {
        livesGameObjects[index].SetActive(false);
    }

    public void SetScore(int amount)
    {
        scoreTxt.text = amount.ToString();
    }

    public void SetHighScore(int amount)
    {
        highScoreTxt.text = amount.ToString();
    }

    public void ResetUI()
    {
        scoreTxt.text = "0";

        foreach (var Life in livesGameObjects)
        {
            Life.SetActive(true);
        }
    }
}
