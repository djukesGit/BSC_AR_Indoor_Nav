using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Text DisplayText;
    public GameObject OCR_Menu;
    public GameObject Room_Menu;
    public GameObject Loading_Menu;
    // public GameObject Loading_Menu;
    //public Button btn_loadingScreen;

    public GameObject LocalPoints;

    private void Start()
    {
        OCR_Menu.SetActive(true);
        Room_Menu.SetActive(false);
        Loading_Menu.SetActive(false);
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
                DisplayText.text = "Wert gefunden :" + value;
                NavData.Start.index = value;
                OCR_Menu.SetActive(false);
                Loading_Menu.SetActive(false);
                Room_Menu.SetActive(true);


            }
            else
            {
                value = -1;
                DisplayText.text = "Wert nicht gefunden :" + value;
            }

            NavData.OCR.finishedOCR = false;
        }

    }

    void showLoadingScreen()
    {
        OCR_Menu.SetActive(false);
        Room_Menu.SetActive(false);

        Loading_Menu.SetActive(true);
    }
}
