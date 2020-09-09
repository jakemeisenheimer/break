using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class encon : MonoBehaviour
{
    public GameObject[] enimes;
    public GameObject wall;
    public GameObject next;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool a = false;
        foreach (GameObject g in enimes)
        {
            a = a | g != null;
        }
        if (!a)
        {
            breakwall();
        }
    }

    public void breakwall()
    {
        next.SetActive(true);
        foreach (Rigidbody r in wall.GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = false;
        }
    }
}
