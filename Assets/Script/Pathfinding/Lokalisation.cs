using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lokalisation : MonoBehaviour
{
    public GameObject ARDevice;
    public GameObject Camera;
    public GameObject StartPosition;

    void Start()
    {
    
        ARDevice.transform.position = StartPosition.transform.GetChild(0).position;
        Camera.transform.rotation = StartPosition.transform.GetChild(0).rotation; ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
