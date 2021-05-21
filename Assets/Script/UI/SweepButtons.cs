using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SweepButtons : MonoBehaviour

{
    public Image Image_Button1;
    public Image Image_Button2;
    public Image Image_Button3;
    public Image Image_Button4;
    public Image Image_Button5;

    private Vector4 m_color;
    private bool reverse = false;
    // Start is called before the first frame update
    private void Start()
    {
        m_color = Image_Button1.color;
    }
    void FixedUpdate()
    {

        alpha_sweep();
        Image_Button1.color = m_color;
        Image_Button2.color = m_color;
        Image_Button3.color = m_color;
        Image_Button4.color = m_color;
        Image_Button5.color = m_color;
    }

    private void alpha_sweep()
    {
        if (m_color.w >= 1f)
        {
            reverse = true;
        }
        else if (m_color.w <= 0f)
        {

            reverse = false;
        }
        else
        {

        }

        if (reverse)
        {
            m_color.w -= 0.01f;
        }
        else
        {
            m_color.w += 0.01f;
        }

    }
}
