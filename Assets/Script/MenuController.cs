using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    
    public GameObject Menu_Start;
    public GameObject Menu_TextRegnozice;
    public GameObject Menu_RoomList;
    public GameObject Loading_Menu;
    public GameObject UI_ScanObjects;
    public Button StartButton;
    // public GameObject Loading_Menu;
    //public Button btn_loadingScreen;

    
    private void Start()
    {
        Menu_Start.SetActive(true);
        Menu_TextRegnozice.SetActive(false);
        Menu_RoomList.SetActive(false);
        Loading_Menu.SetActive(false);
        StartButton.onClick.AddListener(Activ_RoomMenu);
        StartButton.onClick.AddListener(Activ_RoomMenu);
        //btn_loadingScreen.onClick.AddListener(showLoadingScreen);
    }

   void Update()

    {
        
        if (NavData.OCR.finishedOCR)
        {
            int value = -1;
            string text = NavData.OCR.text;

            if (NavData.Room.name.TryGetValue(text, out value))
            {
               // DisplayText.text = "Wert gefunden :" + value;
                NavData.Start.index = value;


                Menu_Start.SetActive(false);
                Menu_TextRegnozice.SetActive(false);
                Loading_Menu.SetActive(false);
               // Menu_RoomList.SetActive(true);


            }
            else
            {
                value = -1;

                Menu_TextRegnozice.SetActive(true);
                UI_ScanObjects.SetActive(true);
                Loading_Menu.SetActive(false);
                //  DisplayText.text = "Wert nicht gefunden :" + value;
            }



            NavData.OCR.finishedOCR = false;
        }

    }

  
    void Activ_TextRegnoMenu()
    {
        Menu_Start.SetActive(false);
        Menu_TextRegnozice.SetActive(true);
        Loading_Menu.SetActive(false);
        Menu_RoomList.SetActive(false);
    }

    void Activ_RoomMenu()
    {
        Menu_Start.SetActive(false);
        Menu_TextRegnozice.SetActive(false);
        Loading_Menu.SetActive(false);
        Menu_RoomList.SetActive(true);
    }


}
