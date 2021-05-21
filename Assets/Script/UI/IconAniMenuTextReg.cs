using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconAniMenuTextReg : MonoBehaviour
{
    public Image ButtonEmissionIcon;
    public Image ButtonTextIcon;

    //public Image FrameAlbedo;
    public Image FrameEmission;
    private bool reverse = false;
    private Vector4 ColorIcon;
    void Start()
    {
        ButtonEmissionIcon.fillAmount = 0;
        FrameEmission.fillAmount = 0;
        FrameEmission.fillOrigin = (int)Image.OriginHorizontal.Left;

        ButtonEmissionIcon.fillClockwise = true;
        ColorIcon = ButtonTextIcon.color;
        ColorIcon.w = 0f;
        ButtonTextIcon.color = ColorIcon;
    }

    // Update is called once per frame
    void Update()
    {
        UI_sweep();
        ButtonTextIcon.color = ColorIcon;
        ButtonEmissionIcon.color = ColorIcon;
    }

    private void UI_sweep()
    {
        if (FrameEmission.fillAmount >= 1f)
        {
           // ButtonEmissionIcon.fillClockwise = false;
            reverse = true;
            FrameEmission.fillOrigin = (int)Image.OriginHorizontal.Right;
        }
        else if (FrameEmission.fillAmount <= 0f)
        {
          //  ButtonEmissionIcon.fillClockwise = true;
            reverse = false;
            FrameEmission.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
        else
        {

        }

        if (reverse)
        {
            ColorIcon.w -= 0.01f;
            //ButtonEmissionIcon.fillAmount -= 0.01f;
            FrameEmission.fillAmount -= 0.01f;
        }
        else
        {
            ColorIcon.w += 0.01f;
           // ButtonEmissionIcon.fillAmount += 0.01f;
            FrameEmission.fillAmount += 0.01f;
        }
    }
}
