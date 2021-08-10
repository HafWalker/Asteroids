using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour {
    
    public AudioManager audioManager;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public float reloadTime = 0;

    public void Shoot() {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
        if (bullet != null) {
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.up * bulletSpeed, ForceMode2D.Impulse);
        }

        audioManager.PlayShootClip();
    }
}