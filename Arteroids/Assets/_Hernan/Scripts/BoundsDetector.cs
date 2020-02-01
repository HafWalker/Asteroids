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
