using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevelopController : MonoBehaviour
{
    public Button btn_Develop;
    public GameObject SimMenu;
    private bool isOpen = false;
    // Start is called before the first frame update
    private void Awake()
    {
        SimMenu.SetActive(false);
    }
    void Start()
    {
        btn_Develop.onClick.AddListener(opencloseSimulationMenu);
    }

    // Update is called once per frame
   
    void opencloseSimulationMenu()
    {
        if (isOpen)
        {
            SimMenu.SetActive(false);
            isOpen = false;
        }
        else
        {
            SimMenu.SetActive(true);
            isOpen = true;
        }
    }
}
