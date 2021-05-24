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
    //public GameObject LocalPoints;
   // public GameObject CheckScreen;

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
        
        if ( NavData.Start.index > -1 && NavData.Device.setPosition) 
        { 
            currentARPosition = Frame.Pose.position;
            if (!Tracking)
            {
                Tracking = true;
                PrevARPosePosition = Frame.Pose.position;
            }

            deltaPosition = currentARPosition - PrevARPosePosition;
            PrevARPosePosition = currentARPosition;


            Camera.transform.Translate(deltaPosition.x, 0.0f, deltaPosition.z);
        }
        else
        {
            return;
        }
    }
}
