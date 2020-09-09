using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public GameObject robot;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hammer")
        {
            foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
            {
                r.isKinematic = false;
                Instantiate(explosion, robot.transform.position, Quaternion.identity);
                Destroy(robot);
            }
        }
        }
    }
