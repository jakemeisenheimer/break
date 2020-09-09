using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = GetComponent<Rigidbody>().velocity;
        if (v.y > 50)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(v.x,50,v.z);
        }
    }
}
