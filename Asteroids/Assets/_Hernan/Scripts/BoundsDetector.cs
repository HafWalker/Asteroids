using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetector : MonoBehaviour
{
    private Camera cam;

    private float left_bound;
    private float right_bound;
    private float top_bound;
    private float bottom_bound;

    private Vector3 templVector;

    void Start()
    {
        cam = FindObjectOfType<Camera>();

        right_bound = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, -1f)).x;
        left_bound = cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).x;
        top_bound = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, -1f)).y;
        bottom_bound = cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).y;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < left_bound) 
        {
            templVector.x = right_bound;
            templVector.y = transform.localPosition.y;
            templVector.z = transform.localPosition.z;
            transform.position = templVector; 
        }
        else if (transform.position.x > right_bound)
        {
            templVector.x = left_bound;
            templVector.y = transform.localPosition.y;
            templVector.z = transform.localPosition.z;
            transform.position = templVector;
        }

        if (transform.position.y > top_bound)
        {
            templVector.x = transform.localPosition.x;
            templVector.y = bottom_bound;
            templVector.z = transform.localPosition.z;
            transform.position = templVector;
        }
        else if (transform.position.y < bottom_bound)
        {
            templVector.x = transform.localPosition.x;
            templVector.y = top_bound;
            templVector.z = transform.localPosition.z;
            transform.position = templVector;
        }
    }
}
