using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float bulletLife = 5;

    private WaitForSeconds cacheWaitToDisable;

    private void OnEnable()
    {
        cacheWaitToDisable = new WaitForSeconds(bulletLife);
        StartCoroutine(CounterToDisable());
    }

    public IEnumerator CounterToDisable() {
        yield return cacheWaitToDisable;
        gameObject.SetActive(false);
    }
}
