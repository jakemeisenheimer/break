using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    public GameObject spear;
    public GameObject throwable;
    public Transform hand;
    public GameObject player;
    public bool hold;
    public Animator anim;
    public float timer;
    public bool ragdolled;
    public GameObject cube;
    public GameObject centerofmass;
    public Vector3 top;
    // Start is called before the first frame update
    void Start()
    {
        top = transform.position + Vector3.up * 3;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 10)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                timer = 0;
                anim.SetBool("throw", true);
            }
            transform.LookAt(player.transform.position);
            if (hold)
            {
                throwable.transform.position = top;
            }
        }
    }

    public void makespear()
    {
        hold = true;
        throwable = Instantiate(spear, top, transform.rotation);
        
    }

    public void throwspear()
    {
       
        anim.SetBool("throw", false);
        hold = false;
        
        
    }

    public void breakcube()
    {
        foreach (Rigidbody g in cube.GetComponentsInChildren<Rigidbody>())
        {
            g.isKinematic = false;
        }
    }

    public void noGravity()
    {
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.useGravity = false;
        }
    }

    public void trueonRagdoll()
    {
        foreach (CapsuleCollider c in GetComponentsInChildren<CapsuleCollider>())
        {
            c.enabled = true;
        }
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = false;

        }
        foreach (BoxCollider c in GetComponentsInChildren<BoxCollider>())
        {
            c.enabled = true;
        }
        GetComponent<Animator>().enabled = false;
    }
}
