using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public AudioManager audioManager;

    public float rotSpeed;
    public float forwardSpeed;
    public float maxVelocity = 100;

    public float timeToReload;

    private Rigidbody2D rigidbody_2D;

    private GameObject propeller;

    public Transform bulletSpawn;
    private bool canFire = true;
    public float bulletSpeed;

    private Vector2 startPos;  

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rigidbody_2D = GetComponent<Rigidbody2D>();
        propeller = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {        
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(Vector3.forward * rotSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * -rotSpeed);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (rigidbody_2D.velocity.magnitude <= maxVelocity)
            {
                rigidbody_2D.AddForce(transform.up * forwardSpeed);
            }
            else
            {
                rigidbody_2D.velocity = Vector2.ClampMagnitude(rigidbody_2D.velocity, maxVelocity-1);
            }

            audioManager.playJetClip();

            propeller.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            propeller.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canFire)
            {
                Shoot();
            }
        }
    }

    public void ResetPos()
    {
        rigidbody_2D.velocity = Vector3.zero;
        rigidbody_2D.angularVelocity = 0;
        transform.position = startPos;
    }

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
        canFire = false;

        audioManager.PlayShootClip();

        StartCoroutine(WaitToReload());
    }

    public void DisableShipRenderer()
    {
        propeller.SetActive(false);
    }

    public IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(timeToReload);
        canFire = true;
    }
}
