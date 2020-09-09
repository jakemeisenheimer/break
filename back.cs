using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back : MonoBehaviour
{
    public GameObject controller;
    public Vector3 origin;
    public Vector3 r;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        r = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetComponent<backcontrol>().reverse)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.eulerAngles = r;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
            transform.position = Vector3.Lerp(transform.position, origin, controller.GetComponent<backcontrol>().lerp);
        }
    }
}
