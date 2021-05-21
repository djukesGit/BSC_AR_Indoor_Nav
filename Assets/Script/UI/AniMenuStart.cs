using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class AniMenuStart : MonoBehaviour
{
    // Start is called before the first frame update
    public Text ButtonText;
    private new Vector4 m_color;
    private bool reverse = false;
    // Update is called once per frame

    private void Start()
    {
        m_color = ButtonText.color;
    }
    void FixedUpdate()
    {
      
        alpha_sweep();
        ButtonText.color = m_color;
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
            m_color.w -= 0.02f;
        }
        else
        {
            m_color.w += 0.02f;
        }

    }

}
