using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCheck : MonoBehaviour
{
    
    public GameObject ARCoreDevice;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        StartCoroutine(WaitCoroutine());
       
    }

    IEnumerator WaitCoroutine()
    {


       
        yield return new WaitForSeconds(3);
        ARCoreDevice.GetComponent<ARCoreSession>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
