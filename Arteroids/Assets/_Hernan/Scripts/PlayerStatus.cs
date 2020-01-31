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
    private bool shielCorutinedCheck = true;
    public GameObject shield;
    public Animator shieldAnimator;

    public int lives;
    public int actualLives;
    public List<GameObject> livesGameObjects;

    public float timeToRespawn;

    public ShipExplosion shipExplosion;

    public void SetScore(int amount)
    {
        scoreAmount += amount;
        scoreTxt.text = scoreAmount.ToString();
    }

    public int GetScore()
    {
        return scoreAmount;
    }

    public void RestoreStatus()
    {
        scoreAmount = 0;
        scoreTxt.text = scoreAmount.ToString();

        actualLives = lives;

        actualLives = livesGameObjects.Count - 1;
        foreach (GameObject life in livesGameObjects)
        {
            life.SetActive(true);
        }

        StopAllCoroutines();

        EnableShield();
    }

    public void Respawn()
    {
        transform.GetChild(1).GetComponent<Image>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<ShipController>().enabled = true;
    }

    public void RemoveLife()
    {
        if (!shield.activeInHierarchy)
        {
            livesGameObjects[actualLives].SetActive(false);

            shipExplosion.Explode(transform.position);

            //Chequear si en la corrutina se llama cont
            transform.GetChild(1).GetComponent<Image>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<ShipController>().enabled = false;
            GetComponent<ShipController>().DisableShipRenderer();

            if (actualLives > 0)
            {
                StartCoroutine(RespawnOnTime(timeToRespawn,false));
                actualLives--;
            }
            else
            {
                StartCoroutine(RespawnOnTime(timeToRespawn,true));
            }
        }
        else
        {
            if (shielCorutinedCheck)
            {
                StartCoroutine(DisableShieldAnimation());
                shielCorutinedCheck = false;
            }
            
        }
    }

    public void EnableShield()
    {
        shieldAnimator.SetBool("ActiveShieldAnimation", true);
        shielCorutinedCheck = true;
        haveShield = true;
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

    public IEnumerator RespawnOnTime(float t, bool isGameOver)
    {
        yield return new WaitForSeconds(t);
        if (!isGameOver)
        {
            gameMgr.ResetWorld();
        }
        else
        {
            gameMgr.SetGameOver();
        }
    }
}
