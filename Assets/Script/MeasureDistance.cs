using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeasureDistance : MonoBehaviour
{
    //   public GameObject ARCoreDevice;
    public Camera ARCoreCam;
    public GameObject Destination;
    public Text values;
    public Text distance;
    private float Dx;
    private float Dz;

    private float Ax;
    private float Az;

    private float Rx;
    private float Rz;
    private float DistanceResult;
    // Start is called before the first frame update
    void Start()
    {
        Ax = 0.0f;
        Az = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (NavData.Room.index < 0)
        {
            return;
        }
        Dx = Destination.transform.GetChild(NavData.Room.index).transform.position.x;
        Dz = Destination.transform.GetChild(NavData.Room.index).transform.position.z;

        Ax = ARCoreCam.transform.position.x;
        Az = ARCoreCam.transform.position.z;

        

        Rx = Dx - Ax;
        Rz = Dz - Az;

        DistanceResult = Mathf.Sqrt((Rx*Rx)+(Rz*Rz));
        values.text = "X: " +Rx+"m"+"   Z: "+Rz+"m";
        distance.text = "Distance: "+DistanceResult+"m";
    }
}
