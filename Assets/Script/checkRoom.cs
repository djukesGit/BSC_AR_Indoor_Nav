using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkRoom : MonoBehaviour
{
    //  public GameObject Room_Menu;
    public GameObject Room_Menu;
    //public Button btn_StdInfoCenter;
    public Button btn_HS1;
    public Button btn_HS2;
    public Button btn_HS3;
    public Button btn_Libary;
    public Text Output;

    // Start is called before the first frame update
    void Start()
    {
       // btn_StdInfoCenter.onClick.AddListener(delegate { setRoomIndex(0); });
        btn_HS1.onClick.AddListener(delegate { setRoomIndex(1); });
        btn_HS2.onClick.AddListener(delegate { setRoomIndex(2); });
        btn_HS3.onClick.AddListener(delegate { setRoomIndex(3); });
        btn_Libary.onClick.AddListener(delegate { setRoomIndex(7); });

       
    }

    private void Update()
    {
        Output.text = "Index: " + NavData.Start.index;
    }
    private void setRoomIndex(int index)
    {
        NavData.Room.index = index;
        Room_Menu.SetActive(false);
    }
}
