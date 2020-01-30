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

    public int actualLives;
    public List<GameObject> lives;

    public float timeToRespawn;

    public ShipExplosion shipExplosion;

    private void Start()
    {
        AddScore(0);
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

    public void Respawn()
    {
        transform.GetChild(1).GetComponent<Image>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void RemoveLife()
    {
        if (!shield.activeInHierarchy)
        {
            lives[actualLives].SetActive(false);

            shipExplosion.Explode(transform.position);

            //Chequear si en la corrutina se llama cont
            transform.GetChild(1).GetComponent<Image>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            StartCoroutine(TimeToRespawn(timeToRespawn));

            if (actualLives > 0)
            {
                actualLives--;
            }
            else
            {
                gameMgr.SetGameOver();
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

    public IEnumerator TimeToRespawn(float t)
    {
        yield return new WaitForSeconds(t);
        gameMgr.ResetWorld();
    }
}
