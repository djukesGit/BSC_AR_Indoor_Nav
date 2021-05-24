using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MiniMapCamMove : MonoBehaviour
{
    public GameObject MiniMapManager;
    public Camera MiniMapCam;
    public GameObject arrow;

    public GameObject ARCoreDevice;
    public Camera ARCoreCam;
    private Vector3 Pos = new Vector3(0.0f, 10f, 0.0f);
    private Vector3 eulRot = new Vector3(0.0f, 0.0f, 0.0f);
    private void OnEnable()
    {
        MiniMapManager.transform.position = ARCoreDevice.transform.position;
        MiniMapManager.transform.rotation = ARCoreDevice.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        Pos.x = ARCoreCam.transform.position.x;
        Pos.z = ARCoreCam.transform.position.z;
        MiniMapCam.transform.position = Pos;
        arrow.transform.position = new Vector3 (Pos.x,1f,Pos.z);
        eulRot.y = ARCoreCam.transform.eulerAngles.y;
        arrow.transform.eulerAngles = eulRot;

    }
}
