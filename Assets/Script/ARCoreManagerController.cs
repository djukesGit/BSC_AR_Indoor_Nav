using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.UI;

public class ARCoreManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn_Scan;
    public Button btn_ReInit;

    public GameObject LoadingScreen;
    public GameObject Menu_ARNav;
    public GameObject Menu_TextReg;

    public GameObject TesseractDemoScript_Prefab;
    public GameObject ARCoreDeviceV2;

    public GameObject DoorPlate_Prefab;

    private Texture2D m_TextureRender;

   

    private ARCoreSession arcoreSession;
   

    public ARCoreSessionConfig myConfigScan;
    public ARCoreSessionConfig myConfigUserTracking;

    public GameObject LocalPoints;

    public GameObject DoorPlate_Menu;
    public Button btn_ScanDoorPlate;

    public Text PosDoorPlate;
    public Text PosUser;

    private bool isDestroyed = false;

    
    void Start()
    {
        btn_Scan.onClick.AddListener(CaptureScreenAsync);
        btn_ReInit.onClick.AddListener(setNewPosition);
        btn_ScanDoorPlate.onClick.AddListener(ScanDoorPlate);

        //Instance_ARDevice = Instantiate(ARCoreDeviceV2_Prefab, Vector3.zero, Quaternion.identity);
        ARCoreDeviceV2.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);//DoorPlate invisible
 
        arcoreSession = ARCoreDeviceV2.GetComponent<ARCoreSession>();
        arcoreSession.SessionConfig=myConfigScan;

        arcoreSession.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (NavData.Start.index > -1 && !isDestroyed && NavData.OCR.newLocalisation)
        {

            ARCoreDeviceV2.transform.rotation = LocalPoints.transform.GetChild(NavData.Start.index).rotation;
            ARCoreDeviceV2.transform.position = LocalPoints.transform.GetChild(NavData.Start.index).position;
            NavData.OCR.newLocalisation = false;
            //Menu_ARNav.SetActive(true);



            DestroyImmediate(arcoreSession);


            DoorPlate_Menu.SetActive(true);
            ARCoreDeviceV2.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);


            arcoreSession = ARCoreDeviceV2.AddComponent<ARCoreSession>();
            arcoreSession.SessionConfig = myConfigUserTracking;
            arcoreSession.enabled = true;
            isDestroyed = true;
        }

        if (NavData.Start.index > -1 && NavData.OCR.newLocalisation)
        {
            ARCoreDeviceV2.transform.rotation = LocalPoints.transform.GetChild(NavData.Start.index).rotation;
            ARCoreDeviceV2.transform.position = LocalPoints.transform.GetChild(NavData.Start.index).position;

            NavData.OCR.newLocalisation = false;
        }


    }

  


    void CaptureScreenAsync()
    {

        var image = Frame.CameraImage.AcquireCameraImageBytes();

        byte[] bufferY = new byte[image.Width * image.Height];
        byte[] bufferU = new byte[image.Width * image.Height / 2];
        byte[] bufferV = new byte[image.Width * image.Height / 2];
        System.Runtime.InteropServices.Marshal.Copy(image.Y, bufferY, 0, image.Width * image.Height);
        System.Runtime.InteropServices.Marshal.Copy(image.U, bufferU, 0, image.Width * image.Height / 2);
        System.Runtime.InteropServices.Marshal.Copy(image.V, bufferV, 0, image.Width * image.Height / 2);


        m_TextureRender = new Texture2D(image.Width, image.Height, TextureFormat.RGBA32, false, false);


        Color c = new Color();
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                float Y = bufferY[y * image.Width + x];
                float U = bufferU[(y / 2) * image.Width + x];
                float V = bufferV[(y / 2) * image.Width + x];
                c.r = Y;
                c.g = Y;
                c.b = Y;

                c.r /= 255.0f;
                c.g /= 255.0f;
                c.b /= 255.0f;

                if (c.r < 0.0f) c.r = 0.0f;
                if (c.g < 0.0f) c.g = 0.0f;
                if (c.b < 0.0f) c.b = 0.0f;

                if (c.r > 1.0f) c.r = 1.0f;
                if (c.g > 1.0f) c.g = 1.0f;
                if (c.b > 1.0f) c.b = 1.0f;

                c.a = 1.0f;
                m_TextureRender.SetPixel(image.Width - 1 - x, y, c);
            }
        }
        Color[] m_color = m_TextureRender.GetPixels();


        m_color = RotateMatrix(m_color, m_TextureRender.width,m_TextureRender.height);
      //  Texture2D _texture = new Texture2D(m_TextureRender.height, m_TextureRender.width);

        // Texture2D _texture = new Texture2D(m_TextureRender.width, m_TextureRender.height);

        NavData.ImageData._highlightedTexture = new Texture2D(480, 640);


        NavData.ImageData._highlightedTexture.SetPixels(m_color);
        NavData.ImageData._highlightedTexture.Apply();
        NavData.ImageData.colors = NavData.ImageData._highlightedTexture.GetPixels32();





       // _texture.SetPixels(m_color);
        var encodedJpg = NavData.ImageData._highlightedTexture.EncodeToJPG();
        var path = Application.persistentDataPath;
        File.WriteAllBytes(path + "/NavData_ARCoreTestSample2.jpg", encodedJpg);

        LoadingScreen.SetActive(true);
        Instantiate(TesseractDemoScript_Prefab);

    }

    private Color[] RotateMatrix(Color[] matrix, int width, int heigth)
    {
        Color[] ret = new Color[width * heigth];

        for (int i = 0; i < heigth; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
               //ret[i + (witdh - 1 - j) * heigth] = matrix[j + i * witdh]; //ccw

                ret[(heigth - 1) - i + j * heigth] = matrix[j + i * width]; //cw
            }
        }

        return ret;
    }

    void ScanDoorPlate()
    {
        PosDoorPlate.text = "DoorPlate: " + ARCoreDeviceV2.transform.position;

        Vector3 setPos;
        setPos= ARCoreDeviceV2.transform.position;
        setPos.z -= 0.4f;
        // ARCoreDeviceV2.transform.position=setPos;
        ARCoreDeviceV2.transform.GetChild(0).transform.position = setPos;

        ARCoreDeviceV2.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        PosUser.text = "User:   " + ARCoreDeviceV2.transform.GetChild(0).transform.position;
        DoorPlate_Menu.SetActive(false);
        Menu_ARNav.SetActive(true);
        NavData.Device.setPosition = true;
    }

    void setNewPosition()
    {
        Menu_ARNav.SetActive(false);
        Menu_TextReg.SetActive(true);
    }
}
