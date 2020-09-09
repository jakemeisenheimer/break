using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex : MonoBehaviour
{
    public Vector3 e;
    // Start is called before the first frame update
    void Start()
    {
        e = new Vector3(6, 6, 6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = e;
            e -= new Vector3(.1f, .1f, .1f);
        if (e.x < 0)
        {
            Destroy(gameObject);
        }
    }
}
