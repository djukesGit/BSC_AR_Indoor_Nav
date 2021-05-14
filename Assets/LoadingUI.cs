using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public Image LoadingIcon;
   // public Text Displaytext;
    private float forward_value;
    private float reverse_value;
    // Start is called before the first frame update
    void Start()
    {
        forward_value=0f;
        reverse_value=1f;
        LoadingIcon.fillClockwise = true;
      //  Displaytext.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (forward_value < 1f)
        {
            LoadingIcon.fillClockwise =true;
            forward_value += 0.015f;
            LoadingIcon.fillAmount = forward_value;
            return;
        }
        else if(reverse_value>0f)
        {
            LoadingIcon.fillClockwise = false;
            reverse_value -= 0.02f;
            LoadingIcon.fillAmount = reverse_value;
            return;
        }
        else
        {
            forward_value = 0f;
            reverse_value = 1f;
        }
        
       
      
    }
}
