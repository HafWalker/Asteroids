using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
    
    public GameManager gameMgr;
    public AudioManager audioManager;
    public SpriteRenderer bodyShip;
    public GameObject shield;
    public Animator shieldAnimator;
    public int lives;
    public float deathDelay;
    public delegate void DeathAction();
    public static event DeathAction OnPlayerDeath;

    private int m_score;
    private int m_actualLives;
    private bool m_haveShield = true;
    private ShipController m_shipController;
    private BoxCollider2D m_boxCollider2d;
    private ShipExplosion m_shipExplosion;
    private WaitForSeconds m_cacheWaitToDeath;
    private WaitForSeconds m_cacheWaitDisableShield;

    public void Awake() {
        m_cacheWaitToDeath = new WaitForSeconds(deathDelay);
        m_cacheWaitDisableShield = new WaitForSeconds(3);
        m_shipController = GetComponent<ShipController>();
        m_boxCollider2d = GetComponent<BoxCollider2D>();
        m_shipExplosion = GetComponentInChildren<ShipExplosion>();
    }

    public void Initialize() {
        m_score = 0;
        m_actualLives = lives - 1;
        gameMgr.gameUI.ResetUI();
        EnableShield();
    }

    public int GetScore() {
        return m_score;
    }

    public void AddScore(int amount) {
        m_score += amount;
        gameMgr.gameUI.SetScore(m_score);
    }

    public void Respawn() {
        audioManager.playShipRespawnClip();
        bodyShip.enabled = true;
        m_boxCollider2d.enabled = true;
        m_shipController.enabled = true;
    }

    public void RemoveLife() {
        if (!shield.activeInHierarchy) {
            gameMgr.gameUI.RestLife(m_actualLives);

            m_shipExplosion.Explode(transform.position);
            audioManager.playShipExplosionClip();

            //Chequear si en la corrutina se llama cont
            bodyShip.enabled = false;
            m_boxCollider2d.enabled = false;
            m_shipController.enabled = false;
            m_shipController.DisableShipRenderer();

            if (m_actualLives > 0) {
                StartCoroutine(DeathCoroutine(false));
                m_actualLives--;
            } else {
                StartCoroutine(DeathCoroutine(true));
            }
        } else {
            if (m_haveShield) {
                audioManager.playShieldHitClip(); // Sonido de escudo desactivandose
                StartCoroutine(DisableShieldAnimation());
                m_haveShield = false;
            }
        }
    }

    public void EnableShield() {
        shieldAnimator.SetBool("ActiveShieldAnimation", true);
        m_haveShield = true;
        shield.SetActive(true);
    }

    public IEnumerator DisableShieldAnimation() {
        shieldAnimator.SetBool("ActiveShieldAnimation", false);
        yield return m_cacheWaitDisableShield;
        shield.SetActive(false);
    }

    public IEnumerator DeathCoroutine(bool isGameOver) {
        yield return m_cacheWaitToDeath;

        if (!isGameOver) {
            gameMgr.ResetStage();
        } else {
            OnPlayerDeath();
        }
    }
}