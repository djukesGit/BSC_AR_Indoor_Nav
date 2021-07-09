using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private GameObject m_Object;
    private Vector3 rot_eulAngl;
    // Start is called before the first frame update
    void Start()
    {
        m_Object = this.gameObject;
        rot_eulAngl = m_Object.transform.eulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rot_eulAngl.y>=360f)
        {
            rot_eulAngl.y = 0f;
        }

        rot_eulAngl.y += 0.4f;

        m_Object.transform.GetChild(0).transform.eulerAngles = rot_eulAngl;
        m_Object.transform.GetChild(1).transform.eulerAngles = -rot_eulAngl;

    }
}
