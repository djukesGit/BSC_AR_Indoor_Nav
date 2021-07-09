﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkRoom : MonoBehaviour
{
    //  public GameObject Room_Menu;
    public GameObject Room_Menu;
    public GameObject TextRegManager;
    //public Button btn_StdInfoCenter;
    public Button btn_HS1;
    public Button btn_HS2;
    public Button btn_HS3;
    public Button btn_Libary;
    // public Text Output;
   // public GameObject Scan;
    public GameObject TextRegno_Menu;
    // Start is called before the first frame update
    void Start()
    {
       // btn_StdInfoCenter.onClick.AddListener(delegate { setRoomIndex(0); });
        btn_HS1.onClick.AddListener(delegate { setRoomIndex(1); });
        btn_HS2.onClick.AddListener(delegate { setRoomIndex(2); });
        btn_HS3.onClick.AddListener(delegate { setRoomIndex(3); });
        btn_Libary.onClick.AddListener(delegate { setRoomIndex(8); });

       
    }

    private void Update()
    {
       // Output.text = "Aktueller Standort\n" + NavData.OCR.text;
    }
    private void setRoomIndex(int index)
    {
        Debug.Log("Click : " + index);
        NavData.Room.index = index;
        TextRegManager.SetActive(true);
     
        Room_Menu.SetActive(false);

    }
}
