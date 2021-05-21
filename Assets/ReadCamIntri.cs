using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;
using System;

public class ReadCamIntri : MonoBehaviour
{

    public Text t1;
    public Text t2;
    public Text t3;

    public Camera Cam;

    bool setVaule = false;
    // Start is called before the first frame update
    void Start()
    {
        t1.text = "FocalLength" + Frame.CameraImage.ImageIntrinsics.FocalLength;
        t2.text = "ImageDimension" + Frame.CameraImage.ImageIntrinsics.ImageDimensions;
        t3.text = "CamSensor" +Cam.sensorSize;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Frame.CameraImage.ImageIntrinsics.FocalLength[0]>0f && (setVaule==false)) {
            t1.text = "FocalLength" + Frame.CameraImage.ImageIntrinsics.FocalLength;
            t2.text = "ImageDimension" + Frame.CameraImage.ImageIntrinsics.ImageDimensions;
            t3.text = "Pr Points" + Frame.CameraImage.ImageIntrinsics.PrincipalPoint;

            float focalLength = Frame.CameraImage.ImageIntrinsics.FocalLength[0];
            Vector2 size = Frame.CameraImage.ImageIntrinsics.ImageDimensions;
            float w = size[0];
            float h = size[1];

            double fovW = (180 / Math.PI) * (2 * Math.Atan(w / (focalLength * 2.0)));
            double fovH = (180 / Math.PI) * (2 * Math.Atan(h / (focalLength * 2.0)));

            Cam.fieldOfView = ToSingle(fovW);
            t2.text = "fovW:" + ToSingle(fovW);
            t3.text = "fovW:" + fovH;

            setVaule = true;
        }
    }

    float ToSingle(double value)
    {
        return (float)value;
    }
}
