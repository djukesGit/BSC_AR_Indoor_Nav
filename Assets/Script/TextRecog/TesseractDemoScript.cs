using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TesseractDemoScript : MonoBehaviour
{
       
    private TesseractDriver _tesseractDriver;
    private string _text = "";
  
    
   // private showImage imageToRecognize;
    public Button btn_Scan;
    public Text DisplayText;
    

    List <Action> functionsToRunInMainThread;


    private void Start()
    {
      btn_Scan.onClick.AddListener(StartOCR);
        functionsToRunInMainThread = new List<Action>();
       
    }

    private void Update()
    {
        while (functionsToRunInMainThread.Count > 0)
        {
            Action someFunc = functionsToRunInMainThread[0];
            functionsToRunInMainThread.RemoveAt(0);
            someFunc();

        }
    }

    private void LateUpdate()
    {
        DisplayText.text = _text;


    }

    
    public void CaptureScreen()
    {
        cutTexture(ScreenCapture.CaptureScreenshotAsTexture());
        var path = Application.persistentDataPath;
        var encodedPng = NavData.ImageData._highlightedTexture.EncodeToPNG();
        File.WriteAllBytes(path + "/Bildschirmaufnahme.png", encodedPng);
      
    }

    private void cutTexture(Texture2D _Screenshot)
    {
        Color[] c = _Screenshot.GetPixels((_Screenshot.width - 1024) / 2, (_Screenshot.height - 512) / 2, 1024, 512, 0);
        NavData.ImageData._highlightedTexture = new Texture2D(1024, 512);
        NavData.ImageData._highlightedTexture.SetPixels(c);
        NavData.ImageData._highlightedTexture.Apply();
        NavData.ImageData.colors = NavData.ImageData._highlightedTexture.GetPixels32();

    }

    private void StartOCR()
    {
        CaptureScreen();
        _tesseractDriver = new TesseractDriver();
        StartThreadedFunction(SlowFunction);
        // Recoginze();
    }

    private void Recoginze()
    {
        
        ClearTextDisplay();
        _tesseractDriver.Setup(OnSetupCompleteRecognize);
    }

    private void OnSetupCompleteRecognize()
    {
        NavData.OCR.text = _tesseractDriver.Recognize();
        NavData.OCR.text = NavData.OCR.text.Replace(" ", "");
        AddToTextDisplay(NavData.OCR.text);
        AddToTextDisplay(_tesseractDriver.GetErrorMessage(), true);
        NavData.OCR.finishedOCR = true;

    }

    private void ClearTextDisplay()
    {
        _text = "";
    }

    private void AddToTextDisplay(string text, bool isError = false)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        _text += (string.IsNullOrWhiteSpace(DisplayText.text) ? "" : "\n") + text;

        if (isError)
            Debug.LogError(text);
        else
            Debug.Log(text);
    }

  
    public void StartThreadedFunction(Action someFunction)
    {
        Thread t = new Thread(new ThreadStart(someFunction));
        t.Start();
    }

    public void MainThreadFunction(Action someFunction)
    {
        //  someFunction();
        functionsToRunInMainThread.Add(someFunction);
    }

    public void SlowFunction()
    {
        Debug.Log("Start Thread");
        _tesseractDriver.Setup(OnSetupCompleteRecognize);

        Action aFunction = () =>
        {

        };

        MainThreadFunction(aFunction);
    }

}