using GoogleARCore;
using GoogleARCoreInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class MoveController : MonoBehaviour
{
    public GameObject ARCoreDevice;
    public GameObject Camera;
    public GameObject LocalPoints;
    public GameObject CheckScreen;

    private bool Tracking = false;
    //private bool setPosition = false;

    private UnityEngine.Vector3 deltaPosition;
    private UnityEngine.Vector3 currentARPosition;
    private UnityEngine.Vector3 PrevARPosePosition;
    private Vector3 offsetRotate;

    
    void Start()
    {
        PrevARPosePosition = UnityEngine.Vector3.zero;
    }

  
    void Update()
    {
        
        if ( NavData.Start.index > -1) 
        { 
            if (NavData.Device.setPosition == false)
            {
                //- ARCoreDevice.transform.position = LocalPoints.transform.GetChild(NavData.Start.index).position;
                //- offsetRotate = LocalPoints.transform.GetChild(NavData.Start.index).eulerAngles;
                //   ARCoreDevice.transform.position = new Vector3(17.2f,0, -24.4          f);
                //Camera.transform.rotation = new Vector3(0,0,0);
                //-  NavData.Device.setPosition = true;

                CheckScreen.SetActive(true);
                ARCoreDevice.transform.rotation = LocalPoints.transform.GetChild(NavData.Start.index).rotation;
                ARCoreDevice.transform.position = LocalPoints.transform.GetChild(NavData.Start.index).position;
                // Camera.transform.position = LocalPoints.transform.GetChild(NavData.Start.index).position;

                ARCoreDevice.GetComponent<ARCoreSession>().enabled = true;
                NavData.Device.setPosition = true;
               
            }

            currentARPosition = Frame.Pose.position;
            if (!Tracking)
            {
                Tracking = true;
                PrevARPosePosition = Frame.Pose.position;
            }

            deltaPosition = currentARPosition - PrevARPosePosition;
            PrevARPosePosition = currentARPosition;


            //ARCoreDevice.transform.Translate(deltaPosition.x, 0.0f, deltaPosition.z);
            //Camera.transform.rotation = Frame.Pose.rotation;
            //Camera.transform.eulerAngles += offsetRotate;
          //  ARCoreDevice.transform.rotation = Frame.Pose.rotation;
            Camera.transform.Translate(deltaPosition.x, 0.0f, deltaPosition.z);
        }
        else
        {
            return;
        }
    }
}
