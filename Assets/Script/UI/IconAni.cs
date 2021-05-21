using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconAni : MonoBehaviour
{
    public Image Icon_Localisation;
    public Image Icon_Satellite;
    public Image Icon_Signal_Low;
    public Image Icon_Signal_Middle;
    public Image Icon_Signal_High;

    private Vector3 rotData_LocalisationIcon;
    private Vector3 rotData_SatelliteIcon;
    private Vector4 Color_Icon_Signal;
    private int iteration_count = 0;
    private int offset_speed = 5;

    private bool reverse = false;

    // Start is called before the first frame update
    void Start()
    {
        rotData_LocalisationIcon = Icon_Localisation.transform.eulerAngles;
        rotData_SatelliteIcon = Icon_Satellite.transform.eulerAngles;
        Color_Icon_Signal = Icon_Signal_Low.color;
        Icon_Signal_Low.enabled = false;
        Icon_Signal_Middle.enabled = false;
        Icon_Signal_High.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        rotate_localisation_icon();
        sweep_signal_icon();
        Icon_Localisation.transform.eulerAngles = rotData_LocalisationIcon;
        Icon_Satellite.transform.eulerAngles = rotData_SatelliteIcon;
    }

    void rotate_localisation_icon()
    {
        if (rotData_LocalisationIcon.y >= 180)
        {
            reverse = true;
        }
        else if (rotData_LocalisationIcon.y <= 0)
        {
            reverse = false;
        }
        else
        {
            
        }

        if (reverse)
        {
            rotData_LocalisationIcon.y -= 1f;
           // rotData_SatelliteIcon.y -= 1f;
            rotData_SatelliteIcon.z -= 0.5f;
        }

        else 
        {
            rotData_LocalisationIcon.y += 1f;
           // rotData_SatelliteIcon.y += 1f;
            rotData_SatelliteIcon.z += 0.5f;
        }
    }

    void sweep_signal_icon()
    {
        if (iteration_count == 4* offset_speed)
        {
            Icon_Signal_Low.enabled = true;

        }

        if (iteration_count == 8* offset_speed)
        {
            Icon_Signal_Middle.enabled = true;
        }

        if(iteration_count == 12* offset_speed)
        {
            Icon_Signal_High.enabled = true;
        }

        if (iteration_count >= 13* offset_speed)
        {
            Icon_Signal_Low.enabled = false;
            Icon_Signal_Middle.enabled = false;
            Icon_Signal_High.enabled = false;
            iteration_count = 0;
            return;
        }

        iteration_count++;
    }
}
