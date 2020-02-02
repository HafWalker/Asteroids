﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    protected GameManager gameMgr;
    
    [SerializeField]
    protected AudioManager audioManager;

    [SerializeField]
    protected Image bodyShip;

    [SerializeField]
    protected GameObject shield;

    [SerializeField]
    protected Animator shieldAnimator;

    [SerializeField]
    protected Text scoreTxt;

    [SerializeField]
    protected int lives;

    [SerializeField]
    protected List<GameObject> livesGameObjects;

    [SerializeField]
    protected float deathDelay;

    public delegate void DeathAction();
    public static event DeathAction OnPlayerDeath;

    private int scoreAmount;
    private int actualLives;
    private bool haveShield = true;
    private ShipController shipController;
    private BoxCollider2D boxCollider2d;
    private ShipExplosion shipExplosion;

    public void Awake()
    {
        shipController = GetComponent<ShipController>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        shipExplosion = GetComponentInChildren<ShipExplosion>();
    }

    public void Initialize()
    {
        //isDead = false;

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
        bodyShip.enabled = true;
        boxCollider2d.enabled = true;
        shipController.enabled = true;
    }

    public void RemoveLife()
    {
        if (!shield.activeInHierarchy)
        {
            livesGameObjects[actualLives].SetActive(false);

            shipExplosion.Explode(transform.position);
            audioManager.playShipExplosionClip();

            //Chequear si en la corrutina se llama cont
            bodyShip.enabled = false;
            boxCollider2d.enabled = false;
            shipController.enabled = false;
            shipController.DisableShipRenderer();

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
            OnPlayerDeath();
        }
    }
}
