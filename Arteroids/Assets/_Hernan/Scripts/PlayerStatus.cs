using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameManager gameMgr;
    private int scoreAmount;
    public Text scoreTxt;

    public bool haveShield = true;
    private bool shielCorutinedCheck = false;
    public GameObject shield;
    public Animator shieldAnimator;

    public int actualLives;
    public List<GameObject> lives;

    private void Start()
    {
        RestoreLives();
        EnableShield();
    }

    public void AddScore(int amount)
    {
        scoreAmount += amount;
        scoreTxt.text = scoreAmount.ToString();
    }

    public void RestoreLives()
    {
        actualLives = lives.Count - 1;
        foreach (GameObject life in lives)
        {
            life.SetActive(true);
        }
    }

    public void RemoveLife()
    {
        if (!shield.activeInHierarchy)
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
        else
        {
            
            if (shielCorutinedCheck)
            {
                StartCoroutine(DisableShieldAnimation());
            }
            else
            {
                shielCorutinedCheck = false;
            }
            
        }

    }

    public void EnableShield()
    {
        shield.SetActive(true);
    }

    public void DisableShield()
    {
        shield.SetActive(false);
    }

    public IEnumerator DisableShieldAnimation()
    {
        shieldAnimator.SetBool("ActiveShieldAnimation", false);
        yield return new WaitForSeconds(3);
        haveShield = false;
        DisableShield();
    }
}
