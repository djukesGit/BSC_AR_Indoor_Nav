using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextRegnController : MonoBehaviour
{

    public GameObject ARCoreDevice;
    public GameObject Menu_TextRegn;
    public GameObject UserPositionManager;
    public GameObject LoadingScreen;
    public GameObject TesseractDemoScript_Prefab;
    public Button btn_Scan;

    public ARCoreSessionConfig myConfigScan;

    private ARCoreSession mySession;
    private Texture2D m_TextureRender;
 


    private void Start()
    {
        btn_Scan.onClick.AddListener(scanImageCam);
        UserPositionManager.SetActive(false);
    }

    void OnEnable()
    {
        Menu_TextRegn.SetActive(true);
        DestroyImmediate(ARCoreDevice.GetComponent<ARCoreSession>());
        mySession = ARCoreDevice.AddComponent<ARCoreSession>();
        mySession.SessionConfig = myConfigScan;
        ARCoreDevice.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        
    }
    void Update()
    {
        
        if (NavData.OCR.newLocalisation)
        {
            NavData.OCR.newLocalisation = false;
            
           //ARCoreDevice.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

            UserPositionManager.SetActive(true);
            Menu_TextRegn.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }


    void scanImageCam()
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


        m_color = RotateMatrix(m_color, m_TextureRender.width, m_TextureRender.height);
        //  Texture2D _texture = new Texture2D(m_TextureRender.height, m_TextureRender.width);

        // Texture2D _texture = new Texture2D(m_TextureRender.width, m_TextureRender.height);

        NavData.ImageData._highlightedTexture = new Texture2D(480, 640);


        NavData.ImageData._highlightedTexture.SetPixels(m_color);
        NavData.ImageData._highlightedTexture.Apply();
        NavData.ImageData.colors = NavData.ImageData._highlightedTexture.GetPixels32();





        // _texture.SetPixels(m_color);
        var encodedJpg = NavData.ImageData._highlightedTexture.EncodeToJPG();
        var path = Application.persistentDataPath;
        File.WriteAllBytes(path + "/TextRegnController_Image.jpg", encodedJpg);

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
}
