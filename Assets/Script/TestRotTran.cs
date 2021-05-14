using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotTran : MonoBehaviour
{

    public GameObject Obj1;
    public GameObject Obj2;
    // Start is called before the first frame update
    void Start()
    {
        Obj2.transform.rotation = Obj1.transform.rotation;
        Obj2.transform.position = Obj1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
