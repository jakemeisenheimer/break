using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieEnemy : MonoBehaviour
{
    public bool ragdolled;
    public GameObject centerOfMass;
    public Animator anim;
    bool chaseplayer = false;
    float jumptime = 0;
    GameObject player;
    bool staggered = false;
    public CapsuleCollider arm;
    float time = 0;
    int direct = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        foreach (CapsuleCollider c in GetComponentsInChildren<CapsuleCollider>())
        {
            c.enabled = false;
        }
        foreach (BoxCollider c in GetComponentsInChildren<BoxCollider>())
        {
            c.enabled = false;
        }
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = true;
        }
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 30)
        {
            time = 0;
            if (Random.Range(1, 10) % 2 == 0)
            {
                direct *= -1;
            }
        }
        if (ragdolled == false && staggered == false)
        {
            if (chaseplayer == false)
            {
                anim.SetBool("walk", true);
                transform.Translate(Vector3.forward * .5f * Time.deltaTime);
                RaycastHit hit;
                Vector3 dir = (player.transform.position - transform.position);
                // Does the ray intersect any objects excluding the player layer
                Debug.DrawRay(transform.position + Vector3.up, player.transform.position - transform.position, Color.black);

                if (Physics.Raycast(transform.position+Vector3.up*.5f, transform.TransformDirection(Vector3.forward), out hit, 5))
                {
                    transform.Rotate(0, 1 * direct, 0);
                }
                else if (Physics.Raycast(transform.TransformDirection(Vector3.forward) * 2 + transform.position + Vector3.up, Vector3.down, out hit, 5, 1 << 9))
                {
                    if (hit.point.y < 0)
                    {
                        transform.Rotate(0, 1 * direct, 0);
                    }
                }
                if (Vector3.Angle(dir, transform.TransformPoint(Vector3.forward) - transform.position) < 40 && !Physics.Raycast(transform.position + Vector3.up * 2, player.transform.position - transform.position, out hit, Vector3.Distance(player.transform.position,transform.position)))
                {
                    chaseplayer = true;
                    anim.SetBool("run", true);
                    anim.SetBool("walk", false);
                }
                if (Vector3.Distance(player.transform.position, transform.position) < 5)
                {

                    chaseplayer = true;
                    anim.SetBool("run", true);
                    anim.SetBool("walk", false);
                }
            }
            else
            {
                if (Vector3.Distance(player.transform.position, transform.position) > 1 && anim.GetBool("attack") == false)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5, 1 << 9))
                    {
                        if (hit.point.y < 0 && jumptime > 1)
                        {
                            anim.SetBool("jump", true);
                            jumptime = 0;
                        }
                        else
                        {

                        }
                        transform.LookAt(player.transform.position);
                        transform.Translate(Vector3.forward * 3f * Time.deltaTime);
                    }

                }
                else if (Vector3.Distance(player.transform.position, transform.position) < 1)
                {
                    
                    anim.SetBool("attack", true);
                }
            }
        }
        jumptime += Time.deltaTime;

    }

    public void trueonRagdoll()
    {
        foreach (CapsuleCollider c in GetComponentsInChildren<CapsuleCollider>())
        {
            c.enabled = true;
        }
        foreach (BoxCollider c in GetComponentsInChildren<BoxCollider>())
        {
            c.enabled = true;
        }
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = false;
           
        }
        GetComponent<Animator>().enabled = false;
       
    }

    public void noGravity()
    {
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hammer")
        {
            Vector3 force = other.transform.position - transform.position;
            GetComponent<Rigidbody>().AddForce(new Vector3(force.x,0,force.z)*-300000);
            trueonRagdoll();
            ragdolled = true;
            Destroy(gameObject, 2f);
        }
        if (other.tag == "thrown")
        {
            anim.SetBool("staggered",true);
            staggered = true;
        }
    }

    public void endAnimation(int number)
    {
        if (number == 0)
        {
            anim.SetBool("jump", false);
        }
        if (number == 1)
        {
            arm.enabled = false;
            anim.SetBool("attack", false);
        }
        if(number == 2)
        {
            staggered = false;
            anim.SetBool("staggered", false);
        }
    }

    public void armatt()
    {
        arm.enabled = true;
    }
}
