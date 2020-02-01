using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameManager gameMgr;
    public AudioManager audioManager;

    private int scoreAmount;
    public Text scoreTxt;

    private bool haveShield = true;
    public GameObject shield;
    public Animator shieldAnimator;

    public int lives;
    private int actualLives;
    public List<GameObject> livesGameObjects;

    public float deathDelay;
    public bool isDead = false;

    public ShipExplosion shipExplosion;

    public void Initialize()
    {
        isDead = false;

        foreach (var Life in livesGameObjects)
        {
            Life.SetActive(false);
        }

        scoreAmount = 0;
        scoreTxt.text = scoreAmount.ToString();

        actualLives = lives-1;

        for (int i = 0; i < lives; i++)
        {
            livesGameObjects[i].SetActive(true);
        }

        StopAllCoroutines();

        EnableShield();
    }

    public void SetScore(int amount)
    {
        scoreAmount += amount;
        scoreTxt.text = scoreAmount.ToString();
    }

    public int GetScore()
    {
        return scoreAmount;
    }

    public void Respawn()
    {
        audioManager.playShipRespawnClip();
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
            audioManager.playShipExplosionClip();

            //Chequear si en la corrutina se llama cont
            transform.GetChild(1).GetComponent<Image>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<ShipController>().enabled = false;
            GetComponent<ShipController>().DisableShipRenderer();

            if (actualLives > 0)
            {
                StartCoroutine(DeathCoroutine(deathDelay,false));
                actualLives--;
            }
            else
            {
                StartCoroutine(DeathCoroutine(deathDelay,true));
            }
        }
        else
        {
            if (haveShield)
            {
                audioManager.playShieldHitClip(); // Sonido de escudo desactivandose
                StartCoroutine(DisableShieldAnimation());
                haveShield = false;
            }
            
        }
    }

    public void EnableShield()
    {
        shieldAnimator.SetBool("ActiveShieldAnimation", true);
        haveShield = true;
        shield.SetActive(true);
    }

    public IEnumerator DisableShieldAnimation()
    {
        shieldAnimator.SetBool("ActiveShieldAnimation", false);
        yield return new WaitForSeconds(3);
        shield.SetActive(false);
    }

    public IEnumerator DeathCoroutine(float t, bool isGameOver)
    {
        yield return new WaitForSeconds(t);
        if (!isGameOver)
        {
            gameMgr.ResetWorld();
        }
        else
        {
            isDead = true;
        }
    }
}
