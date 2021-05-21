using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScanController : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public Button ScanButton;
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
                backCam = new WebCamTexture(devices[i].name, backCam.width, backCam.height);

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

        ScanButton.onClick.AddListener(SaveCamTexture);
    }

    private void Update()
    {
        if (!camAvailable)
        {
            return;
        }
       
     // float ratio = (float)backCam.height / (float)backCam.width;

       // float ratio = 1024 / 512;

       

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f,1f, 1f); //scaleY

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0,orient);

        if (NavData.Device.setPosition == true)
        {
            
            background.gameObject.SetActive(false);
           // text.SetActive(false);
           // scanOverlay.SetActive(false);
           
            this.gameObject.SetActive(false);
        }
    }

    void SaveCamTexture()
    {
        /*
        NavData.ImageData._highlightedTexture = new Texture2D(backCam.width, backCam.height);
        Color[] c = backCam.GetPixels((backCam.width - 1024) / 2, (backCam.height - 512) / 2, 1024, 512);

        NavData.ImageData._highlightedTexture.SetPixels(c);
        NavData.ImageData._highlightedTexture.Apply();

        var path = Application.persistentDataPath;
        var encodedPng = NavData.ImageData._highlightedTexture.EncodeToPNG();
        File.WriteAllBytes(path + "/CamPicTest.png", encodedPng);
        NavData.ImageData.colors = NavData.ImageData._highlightedTexture.GetPixels32();
        */

        Texture2D snap = new Texture2D(backCam.width, backCam.height);
        snap.SetPixels(backCam.GetPixels());
        snap.Apply();

        var path = Application.persistentDataPath;
        var encodedPng = snap.EncodeToPNG();
        File.WriteAllBytes(path + "/CamPicTest.png", encodedPng);
    }
}
