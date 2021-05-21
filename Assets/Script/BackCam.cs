using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class BackCam : MonoBehaviour
{
    private bool isCamAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;
    public RawImage background;
    public AspectRatioFitter fit;
    public Button btn_Scan;
    public GameObject TesseractDemoScript_Prefab;

    private int snap_width = 480;
    private int snap_heigth=480;
    public GameObject LoadingScreen;

    private void Awake()
    {
       // LoadingScreen.SetActive(false);
        this.gameObject.SetActive(false);
    }
    private void Start()
    {
        
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif
        btn_Scan.onClick.AddListener(snap);
        StartCoroutine(CheckAuthentication());
       
    }
    private void Update()
    {
        if (!isCamAvailable)
            return;
        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
    IEnumerator CheckAuthentication()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            defaultBackground = background.texture;
            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length == 0)
            {
                Debug.Log("No camera detected");
                isCamAvailable = false;
            }
            else
            {
                for (int i = 0; i < devices.Length; i++)
                {
                    if (!devices[i].isFrontFacing)
                    {
                        backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                    }
                }
                if (backCam == null)
                {
                    Debug.Log("No back cam found!!");
                    backCam = new WebCamTexture(devices[0].name, Screen.width, Screen.height);
                    //return;
                }
                backCam.Play();
                background.texture = backCam;
                isCamAvailable = true;
            }
        }

        else
        {

        }
    }

    void snap()
    {
        Color[] c = backCam.GetPixels((backCam.width - snap_width) / 2, (backCam.height - snap_heigth) / 2, snap_width, snap_heigth);
       // LoadingScreen.SetActive(true);
        c = RotateMatrix(c, snap_width,snap_heigth);
            
        NavData.ImageData._highlightedTexture = new Texture2D(snap_width, snap_heigth);
        NavData.ImageData._highlightedTexture.SetPixels(c);
        NavData.ImageData._highlightedTexture.Apply();
        NavData.ImageData.colors = NavData.ImageData._highlightedTexture.GetPixels32();

        var path = Application.persistentDataPath;
        var encodedPng = NavData.ImageData._highlightedTexture.EncodeToPNG();
        File.WriteAllBytes(path + "/BackCamPictured.png", encodedPng);
        NavData.ImageData.savePicture = true;

        LoadingScreen.SetActive(true);
        Instantiate(TesseractDemoScript_Prefab);
    }


   
 
 
 static Color[] RotateMatrix(Color[] matrix, int witdh, int heigth)
    {
        Color[] ret = new Color[witdh * heigth];

        for (int i = 0; i < heigth; ++i)
        {
            for (int j = 0; j < witdh; ++j)
            {
                ret[i+(witdh-1-j)*heigth] = matrix[j+i*witdh];
            }
        }
        
        return ret;
    }


}
