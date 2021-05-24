using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARNaviController : MonoBehaviour
{
    public GameObject Menu_ARNavi;
    public GameObject TextRegnManager;
    public GameObject MiniMapManager;
    public Button btn_ReInit;

  
    void Start()
    {
        btn_ReInit.onClick.AddListener(ReInitializePos);
    }

    private void OnEnable()
    {
        Menu_ARNavi.SetActive(true);
        MiniMapManager.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
          
    }

    void ReInitializePos()
    {
        Menu_ARNavi.SetActive(false);
        TextRegnManager.SetActive(true);
        MiniMapManager.SetActive(false);
        NavData.Device.setPosition = false;
        this.gameObject.SetActive(false);
    }
}
