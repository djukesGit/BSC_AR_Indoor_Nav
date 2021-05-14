﻿using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanController : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;
    public Text displayText;

    public GameObject ARCoreDevice;
    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            displayText.text = "No camera detected";
            camAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

            }
        }
        if (backCam == null)
        {
            displayText.text = "No back camera";
            return;
        }
        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }

    private void Update()
    {
        if (!camAvailable)
        {
            return;
        }
       
        float ratio = (float)backCam.width / (float)backCam.height;

       // float ratio = 1024 / 512;

       fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        if (NavData.Device.setPosition == true)
        {
            
            background.gameObject.SetActive(false);
           // text.SetActive(false);
           // scanOverlay.SetActive(false);
           
            this.gameObject.SetActive(false);
        }
    }
}
