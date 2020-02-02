using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    [SerializeField]
    protected AudioManager audioManager;

    [SerializeField]
    protected Transform bulletSpawn;

    [SerializeField]
    protected float bulletSpeed;

    [SerializeField]
    protected float reloadTime = 0; // implementar tiempo de recarga 

    public void Shoot()
    {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.up * bulletSpeed, ForceMode2D.Impulse);
        }

        audioManager.PlayShootClip();
    }
}
