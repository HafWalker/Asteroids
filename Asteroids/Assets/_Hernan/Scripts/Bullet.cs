using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    public float bulletLife = 5;

    private WaitForSeconds m_LifeTime;

    private void OnEnable() {
        m_LifeTime = new WaitForSeconds(bulletLife);
        StartCoroutine(CounterToDisable());
    }

    public IEnumerator CounterToDisable() {
        yield return m_LifeTime;
        gameObject.SetActive(false);
    }
}