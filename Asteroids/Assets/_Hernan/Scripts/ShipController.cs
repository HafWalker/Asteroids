using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    protected AudioManager audioManager;

    [SerializeField]
    protected ShipGun shipGun;

    [SerializeField]
    protected float rotSpeed;

    [SerializeField]
    protected float forwardSpeed;

    [SerializeField]
    protected float maxVelocity = 100;

    private Rigidbody2D rigidbody_2D;
    private GameObject propeller;
    private Vector2 startPos;  

    // Start is called before the first frame update
    void Awake()
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
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * -rotSpeed);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            propeller.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shipGun.Shoot();
        }
    }

    public void ResetPos()
    {
        rigidbody_2D.velocity = Vector3.zero;
        rigidbody_2D.angularVelocity = 0;
        transform.position = startPos;
    }

    public void DisableShipRenderer()
    {
        propeller.SetActive(false);
    }
}
