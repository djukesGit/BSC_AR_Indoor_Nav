using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPositionController : MonoBehaviour
{

    public GameObject ARCoreDevice;
    public GameObject Menu_DoorPlate;
    public GameObject ARNaviManager;
    public GameObject LocalPoints;
  

    public Button btn_ScanDoorPlate;
    public Text DoorPlatePos;
    public Text UserPos;
    

    public ARCoreSessionConfig myConfigUserTracking;
   

    private ARCoreSession mySession;


    private void OnEnable()
    {
        Menu_DoorPlate.SetActive(true);
        DestroyImmediate(ARCoreDevice.GetComponent<ARCoreSession>());
        mySession = ARCoreDevice.AddComponent<ARCoreSession>();
        mySession.SessionConfig = myConfigUserTracking;
        ARCoreDevice.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        

    }
    private void Start()
    {
        btn_ScanDoorPlate.onClick.AddListener(setUserPositiononMap);
    }
    // Update is called once per frame
    void Update()
    {
      
        
    }

   void setUserPositiononMap()
    {
        ARCoreDevice.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        Vector3 currentPos;
        currentPos = LocalPoints.transform.GetChild(NavData.Start.index).position;
        DoorPlatePos.text ="DoorPlate Position: "+ currentPos;
        ARCoreDevice.transform.position = currentPos;


        currentPos.z -= 0.4f;
        ARCoreDevice.transform.GetChild(0).transform.position = currentPos;
        ARCoreDevice.transform.rotation = LocalPoints.transform.GetChild(NavData.Start.index).rotation;
        ARCoreDevice.transform.GetChild(0).transform.eulerAngles = Vector3.zero;
        UserPos.text ="User Position:   " + currentPos;
        


        NavData.OCR.newLocalisation = false;
        NavData.Device.setPosition = true;
        Menu_DoorPlate.SetActive(false);
        ARNaviManager.SetActive(true);
        
        this.gameObject.SetActive(false);
    }
}
