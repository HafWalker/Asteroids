using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float bulletLife = 5;

    private void OnEnable()
    {
        StartCoroutine(CounterToDisable());
    }

    public IEnumerator CounterToDisable() {
        yield return new WaitForSeconds(bulletLife);
        gameObject.SetActive(false);
    }
}
