using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    public Button btn_SetStart;
    void Start()
    {
        btn_SetStart.onClick.AddListener(setStartRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setStartRoom()
    {
        NavData.OCR.text = "Horsaal1";
        NavData.OCR.finishedOCR = true;
    }
}
