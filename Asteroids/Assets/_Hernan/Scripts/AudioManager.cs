using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip explosionClip;
    public AudioClip shipExplosionClip;
    public AudioClip shipRespawnClip;
    public AudioClip shieldHitClip;
    public AudioClip jetClip;
    
    private WaitForSeconds m_cacheWaitJetClip;
    private float m_jetDelay;
    private bool m_canPlay = true;

    private void Start() {
        m_jetDelay = jetClip.length;
        m_cacheWaitJetClip = new WaitForSeconds(m_jetDelay);
    }

    public void PlayShootClip() {
        audioSource.clip = shootClip;
        audioSource.Play();
    }

    public void playExplosionClip() {
        audioSource.clip = explosionClip;
        audioSource.Play();
    }

    public void playShipExplosionClip() {
        audioSource.clip = shipExplosionClip;
        audioSource.Play();
    }

    public void playShipRespawnClip() {
        audioSource.clip = shipRespawnClip;
        audioSource.Play();
    }

    public void playShieldHitClip() {
        audioSource.clip = shieldHitClip;
        audioSource.Play();
    }

    public void playJetClip() {
        if (m_canPlay) {
            audioSource.clip = jetClip;
            audioSource.Play();
            StartCoroutine(WaitToPlay());
            m_canPlay = false;
        }
    }

    public IEnumerator WaitToPlay() {
        yield return m_cacheWaitJetClip;
        m_canPlay = true;
    }
}