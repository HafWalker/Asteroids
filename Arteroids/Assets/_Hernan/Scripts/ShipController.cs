using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float rotSpeed;
    public float forwardSpeed;
    public float maxVelocity = 100;

    private Rigidbody2D rigidbody_2D;

    private GameObject propeller;

    public Transform bulletSpawn;
    public bool canFire = true;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody_2D = GetComponent<Rigidbody2D>();
        propeller = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {        
        if (Input.GetKey(KeyCode.LeftArrow)) {
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
            
            propeller.SetActive(true);
        }
        else
        {
            propeller.SetActive(false);
        }

        // debug
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody_2D.velocity = Vector3.zero;
            rigidbody_2D.angularVelocity = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (canFire)
            {
                Shoot();
            }
        }
    }

    public void Shoot() {
        
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.up * bulletSpeed, ForceMode2D.Impulse);
        }
        canFire = false;
       
        StartCoroutine(WaitToReload());
    }

    public IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(.1f);
        canFire = true;
    }
}
