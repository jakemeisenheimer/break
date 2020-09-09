using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour
{
    public GameObject pos;
    GameObject others;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (others != null)
        {
            others.transform.position = pos.transform.position;
        }
    }

    public void shoot()
    {
        if (others != null)
        {
            others.GetComponent<Rigidbody>().velocity = Vector3.zero;
            others.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
            others = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        others = other.gameObject;
        other.transform.position = pos.transform.position;
    }
}
