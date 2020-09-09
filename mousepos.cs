using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousepos : MonoBehaviour
{
    public GameObject cude;
    public float distance = 4.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit) &&  hit.collider.tag == "cutable")
            {
                cude.GetComponent<meshtest>().cutCube(cude.transform.InverseTransformPoint(hit.point).z);
                Debug.Log("as" + cude.transform.InverseTransformPoint(hit.point));
            }
        }


    }

}
