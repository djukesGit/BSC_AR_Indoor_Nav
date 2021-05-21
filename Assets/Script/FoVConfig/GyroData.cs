using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroData : MonoBehaviour
{
    public Text DisplayText;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText.text = "GyroData: " + Input.gyro.attitude.eulerAngles;
        
    }
}
