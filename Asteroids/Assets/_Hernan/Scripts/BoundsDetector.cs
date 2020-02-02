using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetector : MonoBehaviour
{
    private float bounds_height;
    private float bounds_width;

    private float left_bound;
    private float right_bound;
    private float top_bound;
    private float bottom_bound;

    private Vector3 templVector;

    void Start()
    {
        bounds_height = Screen.height*2;
        bounds_width = Screen.width*2;

        right_bound = bounds_width / 2;
        left_bound = -right_bound;
        top_bound = bounds_height / 2;
        bottom_bound = -top_bound;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < left_bound) 
        {
            templVector.x = right_bound;
            templVector.y = transform.localPosition.y;
            templVector.z = transform.localPosition.z;
            transform.localPosition = templVector; 
        }
        else if (transform.localPosition.x > right_bound)
        {
            templVector.x = left_bound;
            templVector.y = transform.localPosition.y;
            templVector.z = transform.localPosition.z;
            transform.localPosition = templVector;
        }

        if (transform.localPosition.y > top_bound)
        {
            templVector.x = transform.localPosition.x;
            templVector.y = bottom_bound;
            templVector.z = transform.localPosition.z;
            transform.localPosition = templVector;
        }
        else if (transform.localPosition.y < bottom_bound)
        {
            templVector.x = transform.localPosition.x;
            templVector.y = top_bound;
            templVector.z = transform.localPosition.z;
            transform.localPosition = templVector;
        }
    }
}
