using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public GameObject rock;
    public Transform rockpos;
    public GameObject r;
    public bool grab = false;
    public float rocktime = 0;
    public Transform hand;
    public Transform foot;
    public float resttime = 0;
    public GameObject wall;
    public SphereCollider a;
    public int health = 1000;
    public int even = 0;
    public GameObject zombie;
    public GameObject robot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            anim.SetBool("death", true);
            anim.SetBool("walk", false);
        }
        if (rocktime > 10)
        {
            anim.SetBool("walk", false);
            anim.SetBool("throw", true);
            rocktime = 0;
        }
        if (anim.GetBool("walk"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (grab == false)
        {
            transform.LookAt(player.transform.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        rocktime += Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) < 1.5)
        {
            anim.SetBool("walk", false);
            anim.SetBool("grab", true);
        }
        if (grab)
        {
            foot.transform.position = hand.position;
        }
        if (resttime > 60 && Vector3.Distance(player.transform.position, transform.position) > 10)
        {
            anim.SetBool("walk", false);
            anim.SetBool("rest", true);
        }
        resttime += Time.deltaTime;
    }

    public void grabrock()
    {
        anim.SetBool("grab", false);
        r =  Instantiate(rock,rockpos.position, Quaternion.identity);
      r.transform.parent = rockpos;
    }

    public void throwrock()
    {
        r.transform.parent = null;
        r.GetComponent<Rigidbody>().useGravity = true;
        r.GetComponent<Rigidbody>().AddForce((player.transform.position + Vector3.up *2 - r.transform.position) * 10000);
        anim.SetBool("throw", false);
        anim.SetBool("walk", true);
    }

    public void endAnimation()
    {
        
    }

    public void relase()
    {
        player.GetComponent<playerCon>().health -= 100;
        grab = false;
        anim.SetBool("walk", true);
    }

    public void death()
    {
        Application.Quit();
        Destroy(gameObject);
    }

    public void grabb()
    {
        a.enabled = true;
        anim.SetBool("grab", false);
    }

    public void makewall()
    {
        
        anim.SetBool("rest", false);
        resttime = 0;
        GameObject w = Instantiate(wall, transform.position - Vector3.up*5, Quaternion.identity);
        if (even % 2 == 0)
        {
            Instantiate(zombie, transform.position + Vector3.up * 5 + transform.right * 8, Quaternion.identity);
            Instantiate(zombie, transform.position + Vector3.up * 5 - transform.right * 8, Quaternion.identity);
            Instantiate(zombie, transform.position + Vector3.up * 5 + transform.forward * 8, Quaternion.identity);
        }
        else
        {
            Instantiate(robot, transform.position  + transform.right * 8, Quaternion.identity);
            Instantiate(robot, transform.position  - transform.right * 8, Quaternion.identity);
        }
        even++;
        w.transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    public void miss()
    {
        if (!grab)
        {
            anim.SetBool("hold", false);
            anim.SetBool("walk", true);
            a.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "thrown")
        {
            health -= 10;
        }
        if (other.tag == "hammer")
        {
            health -= 50;
            other.enabled = false;
        }
    }
    }
