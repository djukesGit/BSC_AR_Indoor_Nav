using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavData
{
    public struct Room
    {
        public static Dictionary<string, int> name = new Dictionary<string, int>()
      {
            //___________________T01_Gebäude_________________
          { "Nothing",0},
          {"Horsaal1",1},
          {"Horsaal2",2},
          {"Horsaal3",3},
          {"Horsaal4",4},
          {"Horsaal5",5},
          {"AudiMax",6},
          {"Bibliothek",7},
          {"Seminarraum1",8},
          {"Mensa",9},
          {"Studien-Info-Center",10},

            //____________________Demo Home___________________
            
            /*
            { "Nothing",0},
          {"Raum1",1},
          {"Raum2",2},
          */
      };

        public static int index = -1;
    }

    public struct Start
    {
        public static int index = -1;
    }

    public struct OCR
    {
        public static string text = "";
        public static bool finishedOCR = false;
    }

    public struct Device
    {
        public static bool setPosition = false;
       //public static bool ready = false;
    }

    public struct ImageData {

        public static Texture2D _highlightedTexture;
        public static int width = 480;//1024
        public static int height = 480;//512
        public static Color32[] colors;
        public static string TextfromImage = "";
        public static bool savePicture = false;

    }
}
