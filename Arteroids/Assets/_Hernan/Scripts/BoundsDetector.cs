using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetector : MonoBehaviour
{
    public float bounds_height;
    public float bounds_width;

    public float left_bound;
    public float right_bound;
    public float top_bound;
    public float bottom_bound;

    void Start()
    {
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
            transform.localPosition = new Vector3(right_bound, transform.localPosition.y, transform.localPosition.z);
        }

        if (transform.localPosition.x > right_bound)
        {
            transform.localPosition = new Vector3(left_bound, transform.localPosition.y, transform.localPosition.z);
        }

        if (transform.localPosition.y > top_bound)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, bottom_bound, transform.localPosition.z);
        }

        if (transform.localPosition.y < bottom_bound)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, top_bound, transform.localPosition.z);
        }

        
    }
}
