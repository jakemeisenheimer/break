using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    public GameObject po;
    public Vector3 pos;
    public uicon uicontroller;
    // Start is called before the first frame update
    void Start()
    {
        uicontroller = GameObject.FindGameObjectWithTag("ui").GetComponent<uicon>();
        pos = transform.position;
        po = (GameObject)Resources.Load("pot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "grabbed" && other.tag == "ground" || other.tag == "thrown" || other.tag == "hammer")
        {
            foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
            {
                r.isKinematic = false;
            }
            foreach (MeshCollider r in GetComponentsInChildren<MeshCollider>())
            {
                r.isTrigger = false;
            }
            Instantiate(po, pos, Quaternion.identity);
            Destroy(gameObject, 1.5f);
        }
        if (other.tag == "thrown" && uicontroller.currenttext == 0)
        {
            uicontroller.next();
        }
        if ( other.tag == "hammer" && uicontroller.currenttext == 1)
        {
            uicontroller.next();
        }
        if (gameObject.tag == "grabbed" && other.tag == "ground" && uicontroller.currenttext == 2)
        {
            uicontroller.next();
        }
    }
    }
