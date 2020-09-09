using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    public GameObject bossm;
    
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
        if (other.tag == "Player")
        {
            bossm.GetComponent<boss>().rocktime = 0;
            GetComponent<SphereCollider>().enabled = false;
            bossm.GetComponent<boss>().anim.SetBool("hold",true);
            bossm.GetComponent<boss>().grab = true;
            other.GetComponent<playerCon>().ragdolled = true;
        }
    }
    }
