using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCheck : MonoBehaviour
{
    
   
    void Update()
    {
        StartCoroutine(WaitCoroutine());
       
    }

    IEnumerator WaitCoroutine()
    {


       
        yield return new WaitForSeconds(3);
      
        this.gameObject.SetActive(false);
    }
}
