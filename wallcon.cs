using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallcon : MonoBehaviour
{
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1)
        {
            transform.position = transform.position + Vector3.up * Time.deltaTime * 6;
        }
        if (timer > 20)
        {
            transform.position = transform.position - Vector3.up * Time.deltaTime * 6;
        }
        if (timer > 22)
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("boss").GetComponent<boss>().anim.SetBool("walk", true);
        }
        timer += Time.deltaTime;
       
    }
}
