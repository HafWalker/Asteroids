using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    protected AudioSource audioSource;

    [SerializeField]
    protected AudioClip shootClip;

    [SerializeField]
    protected AudioClip explosionClip;

    [SerializeField]
    protected AudioClip shipExplosionClip;

    [SerializeField]
    protected AudioClip shipRespawnClip;

    [SerializeField]
    protected AudioClip shieldHitClip;

    [SerializeField]
    protected AudioClip jetClip;
    
    private float jetDelay = 0.25f;
    private bool canPlay = true;

    private void Start()
    {
        jetDelay = jetClip.length;
    }

    public void PlayShootClip()
    {
        audioSource.clip = shootClip;
        audioSource.Play();
    }

    public void playExplosionClip()
    {
        audioSource.clip = explosionClip;
        audioSource.Play();
    }

    public void playShipExplosionClip()
    {
        audioSource.clip = shipExplosionClip;
        audioSource.Play();
    }

    public void playShipRespawnClip()
    {
        audioSource.clip = shipRespawnClip;
        audioSource.Play();
    }

    public void playShieldHitClip()
    {
        audioSource.clip = shieldHitClip;
        audioSource.Play();
    }

    public void playJetClip()
    {
        if (canPlay)
        {
            audioSource.clip = jetClip;
            audioSource.Play();
            StartCoroutine(WaitToPlay(jetDelay));
            canPlay = false;
        }
    }

    public IEnumerator WaitToPlay(float t)
    {
        yield return new WaitForSeconds(t);
        canPlay = true;
    }
}
