using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    
    public AudioManager audioManager;
    public ShipGun shipGun;
    public float rotSpeed;
    public float forwardSpeed;
    public float maxVelocity = 100;
    public Rigidbody2D rigidbody_2D;
    public GameObject propeller;
    
    private Vector2 m_startPos;  

    void Awake() {
        m_startPos = transform.position;
        rigidbody_2D = GetComponent<Rigidbody2D>();
        propeller = transform.GetChild(0).gameObject;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            shipGun.Shoot();
        }
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.forward * rotSpeed);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(Vector3.forward * -rotSpeed);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            propeller.SetActive(false);
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            if (rigidbody_2D.velocity.magnitude <= maxVelocity) {
                rigidbody_2D.AddForce(transform.up * forwardSpeed);
            } else {
                rigidbody_2D.velocity = Vector2.ClampMagnitude(rigidbody_2D.velocity, maxVelocity-1);
            }
            audioManager.playJetClip();
            propeller.SetActive(true);
        }
    }

    public void ResetPos() {
        rigidbody_2D.velocity = Vector3.zero;
        rigidbody_2D.angularVelocity = 0;
        transform.position = m_startPos;
    }

    public void DisableShipRenderer() {
        propeller.SetActive(false);
    }
}