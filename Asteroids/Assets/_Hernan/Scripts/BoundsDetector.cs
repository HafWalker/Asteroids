using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDetector : MonoBehaviour {

    private Camera m_cam;
    private float m_left_bound;
    private float m_right_bound;
    private float m_top_bound;
    private float m_bottom_bound;
    private Vector3 m_templVector;

    void Start() {
        m_cam = FindObjectOfType<Camera>();
        m_right_bound = m_cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, -1f)).x;
        m_left_bound = m_cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).x;
        m_top_bound = m_cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, -1f)).y;
        m_bottom_bound = m_cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).y;
    }

    void Update() {
        if (transform.position.x < m_left_bound) {
            m_templVector.x = m_right_bound;
            m_templVector.y = transform.localPosition.y;
            m_templVector.z = transform.localPosition.z;
            transform.position = m_templVector; 
        }
        else if (transform.position.x > m_right_bound)
        {
            m_templVector.x = m_left_bound;
            m_templVector.y = transform.localPosition.y;
            m_templVector.z = transform.localPosition.z;
            transform.position = m_templVector;
        }

        if (transform.position.y > m_top_bound)
        {
            m_templVector.x = transform.localPosition.x;
            m_templVector.y = m_bottom_bound;
            m_templVector.z = transform.localPosition.z;
            transform.position = m_templVector;
        }
        else if (transform.position.y < m_bottom_bound)
        {
            m_templVector.x = transform.localPosition.x;
            m_templVector.y = m_top_bound;
            m_templVector.z = transform.localPosition.z;
            transform.position = m_templVector;
        }
    }
}