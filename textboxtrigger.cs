using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textboxtrigger : MonoBehaviour
{

    public string[] textlist;
    public KeyCode[] keylist;
    public uicon Uicontroller;
    public GameObject wall;
    public bool boss;
    public GameObject scale;
    public GameObject bosss;
    public GameObject bb;
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
            Uicontroller.testlist = textlist;
            Uicontroller.o = true;
            Uicontroller.keylist = keylist;
            Uicontroller.wall = wall;
            if (boss)
            {
                bb.SetActive(true);
                bosss.SetActive(true);
                scale.transform.localScale = new Vector3(1.4f, 1, 1);
                Destroy(gameObject);
            }
        }
    }
    }
