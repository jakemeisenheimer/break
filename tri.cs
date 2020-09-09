using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tri : MonoBehaviour
{
    public GameObject camer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
        camer.GetComponent<cam>().first = true;
        
    }

    void OnTriggerExit(Collider other)
    {
        
        camer.GetComponent<cam>().first = false;
    }
}
