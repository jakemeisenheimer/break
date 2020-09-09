using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    public GameObject l;
    public string colcolor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("col");
        if (collision.gameObject.tag == colcolor)
        {
            l.SetActive(true);
        }
        
    }
}
