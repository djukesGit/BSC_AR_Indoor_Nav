using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoVCamConfig : MonoBehaviour
{

    public Camera Cam;
    //public Text FoVText;

    bool setVaule = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Frame.CameraImage.ImageIntrinsics.FocalLength[0] > 0f && (setVaule == false))
        {
          
            float focalLength = Frame.CameraImage.ImageIntrinsics.FocalLength[0];
            Vector2 size = Frame.CameraImage.ImageIntrinsics.ImageDimensions;
            float w = size[0];
            float h = size[1];

            double fovW = (180 / System.Math.PI) * (2 * Math.Atan(w / (focalLength * 2.0)));
            double fovH = (180 / Math.PI) * (2 * Math.Atan(h / (focalLength * 2.0)));

            Cam.fieldOfView = ToSingle(fovW);

           // FoVText.text = "FoV: " + Cam.fieldOfView + "    FocalLength: "+Frame.CameraImage.ImageIntrinsics.FocalLength + "    PrincPoints: "+Frame.CameraImage.ImageIntrinsics.PrincipalPoint;
            setVaule = true;

            this.gameObject.SetActive(false);
        }

    }

    float ToSingle(double value)
    {
        return (float)value;
    }
}
