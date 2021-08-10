using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour {
    
    public TextMeshPro scoreTxt;
    public List<GameObject> livesGameObjects;
    public TextMeshPro highScoreTxt;

    public void RestLife(int index) {
        livesGameObjects[index].SetActive(false);
    }

    public void SetScore(int amount) {
        scoreTxt.text = amount.ToString();
    }

    public void SetHighScore(int amount) {
        highScoreTxt.text = amount.ToString();
    }

    public void ResetUI() {
        scoreTxt.text = "0";

        foreach (var Life in livesGameObjects) {
            Life.SetActive(true);
        }
    }
}