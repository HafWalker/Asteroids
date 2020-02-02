using System.Collections;
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
    protected SpriteRenderer bodyShip;

    [SerializeField]
    protected GameObject shield;

    [SerializeField]
    protected Animator shieldAnimator;

    [SerializeField]
    protected int lives;

    [SerializeField]
    protected float deathDelay;

    private int score;

    public delegate void DeathAction();
    public static event DeathAction OnPlayerDeath;

    private int actualLives;
    private bool haveShield = true;
    private ShipController shipController;
    private BoxCollider2D boxCollider2d;
    private ShipExplosion shipExplosion;
    private WaitForSeconds cacheWaitToDeath;
    private WaitForSeconds cacheWaitDisableShield;

    public void Awake()
    {
        cacheWaitToDeath = new WaitForSeconds(deathDelay);
        cacheWaitDisableShield = new WaitForSeconds(3);
        shipController = GetComponent<ShipController>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        shipExplosion = GetComponentInChildren<ShipExplosion>();
    }

    public void Initialize()
    {
        score = 0;
        actualLives = lives - 1;
        gameMgr.gameUI.ResetUI();
        EnableShield();
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        gameMgr.gameUI.SetScore(score);
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
            gameMgr.gameUI.RestLife(actualLives);

            shipExplosion.Explode(transform.position);
            audioManager.playShipExplosionClip();

            //Chequear si en la corrutina se llama cont
            bodyShip.enabled = false;
            boxCollider2d.enabled = false;
            shipController.enabled = false;
            shipController.DisableShipRenderer();

            if (actualLives > 0)
            {
                StartCoroutine(DeathCoroutine(false));
                actualLives--;
            }
            else
            {
                StartCoroutine(DeathCoroutine(true));
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
        yield return cacheWaitDisableShield;
        shield.SetActive(false);
    }

    public IEnumerator DeathCoroutine(bool isGameOver)
    {
        yield return cacheWaitToDeath;

        if (!isGameOver)
        {
            gameMgr.ResetStage();
        }
        else
        {
            OnPlayerDeath();
        }
    }
}
