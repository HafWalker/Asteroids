using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public float bulletLife = 5;

    private WaitForSeconds m_cacheWaitToDisable;

    private void OnEnable() {
        m_cacheWaitToDisable = new WaitForSeconds(bulletLife);
        StartCoroutine(CounterToDisable());
    }

    public IEnumerator CounterToDisable() {
        yield return m_cacheWaitToDisable;
        gameObject.SetActive(false);
    }
}