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
    private GameObject bullet;

    // debug

    public float velMag;

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
            velMag = rigidbody_2D.velocity.magnitude;
            
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot() { 
        //spawn bullet
    }
}
