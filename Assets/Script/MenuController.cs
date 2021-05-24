using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    
    public GameObject Menu_Start;
    public GameObject Menu_RoomList;
    public GameObject LoadingScreen;
    public GameObject CheckScreen;

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

                CheckScreen.SetActive(true);
                Menu_Start.SetActive(false);
                LoadingScreen.SetActive(false);
                // Menu_RoomList.SetActive(true);

                NavData.OCR.newLocalisation = true;

            }
            else
            {
                value = -1;
                LoadingScreen.SetActive(false);

                //  DisplayText.text = "Wert nicht gefunden :" + value;
            }

            

            NavData.OCR.finishedOCR = false;
        }

    }

  
   

    void Activ_RoomMenu()
    {
        Menu_Start.SetActive(false);
        Menu_RoomList.SetActive(true);
    }


}
