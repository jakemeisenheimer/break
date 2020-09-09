using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lever : MonoBehaviour
{
    public GameObject roate;
    public bool dir = false;
    public bool t = false;
    public bool can = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        }

    void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKeyDown(KeyCode.R) && t)
        {
            Debug.Log("r");
            if (dir == false)
            {
                
                roate.transform.Rotate(90, 0, 0);
                dir = true;
            }
            else if (dir == true)
            {
                
                roate.transform.Rotate(-90, 0, 0);
                dir = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && can)
        {
            roate.GetComponent<cannon>().shoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "hammer")
        {
            other.enabled = false;
            roate.SetActive(false);
            foreach (BoxCollider b in GetComponentsInChildren<BoxCollider>())
            {
                b.enabled = true;
            }
            foreach (Rigidbody b in GetComponentsInChildren<Rigidbody>())
            {
                b.useGravity = true;
            }
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
