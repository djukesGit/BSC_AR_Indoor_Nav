using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    
    public GameObject Menu_Start;
    public GameObject Menu_RoomList;
    public GameObject LoadingScreen;

    public Button StartButton;
    // public GameObject Loading_Menu;
    //public Button btn_loadingScreen;

    
    private void Start()
    {
        Menu_Start.SetActive(true);
        Menu_RoomList.SetActive(false);

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
               
                // Menu_RoomList.SetActive(true);

                NavData.OCR.newLocalisation = true;

            }
            else
            {
                value = -1;

            
                //  DisplayText.text = "Wert nicht gefunden :" + value;
            }

            LoadingScreen.SetActive(false);

            NavData.OCR.finishedOCR = false;
        }

    }

  
   

    void Activ_RoomMenu()
    {
        Menu_Start.SetActive(false);
        Menu_RoomList.SetActive(true);
    }


}
