using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerCon : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public GameObject wave;
    public GameObject hammer;
    public List<GameObject> rocks;
    public Vector3[] circlepos;
    bool pickup = false;
    bool jumpattackstart = false;
    public Vector3[] chainpos;
    public Vector3[] chainposfin;
    public float time = 0;
    public bool chainattack = false;
    public bool chainfin = false;
    public float rockTime = 0;
    public int reverse = 1;
    public float xslope = 0;
    public float zslope = 0;
    bool chiainAttackend = false;
    public float fasterAtStart = .5f;
    public GameObject grabedenemy;
    bool rotated = false;
    bool dodge = false;
    bool sheilds = false;
    Vector3 dodgedirection;
    public GameObject mcamera;
    public bool ragdolled = false;
    public bool ragdolloff = false;
    public bool test = false;
    public GameObject seccamera;
    public GameObject hips;
    public bool jumping = false;
    public int health = 200;
    public GameObject sheildgam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        generatreCircle();
    }

    // Update is called once per frame
    void Update()
    {

        if (health < 0)
        {
            SceneManager.LoadScene("end");
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl) && !checkanim())
        {
            if (grabedenemy == null && !chainattack)
            {
                if (rocks.Count > 10)
                {
                    chainattack = true;
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (rocks[rocks.Count - 1].GetComponent<firstrock>() == null)
                        {
                            rocks[rocks.Count - 1].AddComponent<firstrock>();
                            rocks[rocks.Count - 1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

                        }
                        xslope = hit.point.x - transform.position.x;
                        zslope = hit.point.z - transform.position.z;
                        float t = Mathf.Abs(zslope) + Mathf.Abs(xslope);
                        xslope = xslope / t;
                        zslope = zslope / t;
                    }
                }
            }
            else if (chiainAttackend && !chainfin)
            {
                reverse = 1;
                fasterAtStart = 0;
                time = 0;
                chainfin = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && !checkanim())
        {

            if (rocks.Count > 1)
            {
                anim.SetBool("throw", true);
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    xslope = hit.point.x - transform.position.x;
                    zslope = hit.point.z - transform.position.z;
                    float t = Mathf.Abs(zslope) + Mathf.Abs(xslope);
                    xslope = xslope / t;
                    zslope = zslope / t;

                }
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0) && dodge == false && (!checkanim() || anim.GetBool("standingAttack")))
        {
            anim.SetBool("slide", true);
            dodge = true;
            dodgedirection = new Vector3(Mathf.Ceil(Input.GetAxis("Horizontal")), 0, Mathf.Ceil(Input.GetAxis("Vertical")));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            reverse = 1;
            fasterAtStart = .5f;
            time = 0;
        }

        if (pickup)
        {
            makecircle();
        }
        if (sheilds)
        {

            sheild();
        }
        if (chainattack)
        {
            if (chainfin == false)
            {
                chaingrab();
            }
            else
            {
                chainfinish();
            }
        }
        if (jumping)
        {
            jump();
        }
        if (dodge)
        {
            if (rocks.Count > 16)
            {
                dodgeback(-dodgedirection);
            }
        }
    }

    public void throwrock()
    {
        GameObject r = rocks[0];
        rocks.Remove(r);
        Debug.Log(new Vector3(xslope, 0, zslope));
        r.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log(mcamera.transform.localPosition);
        r.transform.position = transform.position + mcamera.transform.localPosition / 5;
        r.GetComponent<MeshCollider>().isTrigger = true;
        r.GetComponent<Rigidbody>().AddForce(new Vector3(xslope, 0, zslope) * 2000);
        r.tag = "thrown";
    }

    void FixedUpdate()
    {
        if ((Mathf.Abs(Input.GetAxis("Horizontal")) > .2 || Mathf.Abs(Input.GetAxis("Vertical")) > .2) && !checkanim() && !test)
        {

            gameObject.transform.eulerAngles = new Vector3(0, caluaterot() + mcamera.GetComponent<cam>().y, 0);

            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
            anim.SetBool("run", true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("attack", true);
                anim.SetBool("run", false);
                jumpattackstart = true;
            }
        }
        else
        {
            anim.SetBool("run", false);
        }
        if (Input.GetKeyDown(KeyCode.E) && !checkanim())
        {
            anim.SetBool("attack", true);
            anim.SetBool("run", false);
            jumpattackstart = true;
        }
        if (Input.GetKeyDown(KeyCode.M) && !checkanim())
        {
            anim.SetBool("down", true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject[] newrocks = GameObject.FindGameObjectsWithTag("rock");
            for (int i = 0; i < newrocks.Length; i++)
            {

                if (rocks.Count < 41)
                {
                    if (Vector3.Distance(transform.position, newrocks[i].transform.position) < 15) {
                        newrocks[i].GetComponent<Rigidbody>().useGravity = false;
                        rocks.Add(newrocks[i]);
                        newrocks[i].tag = "circlerock";
                    }
                }
                else
                {
                    break;
                }
            }
            pickup = true;
            rockTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.V) && !checkanim() )
        {
            GetComponent<Rigidbody>().isKinematic = true;
            anim.SetBool("standingAttack", true);
        }
        if (Input.GetKeyDown(KeyCode.Z) && !checkanim())
        {
            mcamera.SetActive(false);
            seccamera.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            mcamera.SetActive(true);
            seccamera.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            time = 0;
            reverse = 1;
            sheilds = true;
            sheildgam.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            reverse = -1;
            sheildgam.SetActive(false);
        }
       

    }

    float caluaterot()
    {
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        if (hor == 0)
        {
            if (vert > 0)
            {
                return 0;
            }
            if (vert < 0)
            {
                return 180;
            }
        }
        if (vert == 0)
        {
            if (hor > 0)
            {
                return 90;
            }
            if (hor < 0)
            {
                return 270;
            }
        }
        if (vert > 0 && hor > 0)
        {
            return (90 * Mathf.Abs(hor)) / (Mathf.Abs(hor) + Mathf.Abs(vert));
        }
        if (vert > 0 && hor < 0)
        {
            return (-90 * Mathf.Abs(hor)) / (Mathf.Abs(hor) + Mathf.Abs(vert));
        }
        if (vert < 0 && hor > 0)
        {
            return (90 * Mathf.Abs(hor) + 180 * Mathf.Abs(vert)) / (Mathf.Abs(hor) + Mathf.Abs(vert));
        }
        if (vert < 0 && hor < 0)
        {
            return (270 * Mathf.Abs(hor) + 180 * Mathf.Abs(vert)) / (Mathf.Abs(hor) + Mathf.Abs(vert));
        }
        return 0;
    }

    public void jumpattack(int i)
    {
        if (i == 0)
        {
            hammer.GetComponent<CapsuleCollider>().enabled = true;
            
        }
        if (i == 1)
        {
            hammer.GetComponent<BoxCollider>().enabled = true;
            anim.SetBool("standingAttack", false);
        }
    }

    public void Createwave()
    {
        Instantiate(wave, transform.position + transform.forward, Quaternion.identity);
    }

    private void generatreCircle()
    {
        circlepos = new Vector3[72];
        int index = 0;
        for (float i = 3; i < 12; i++)
        {
            for (float x = .5f; x < i; x++)
            {
                circlepos[index] = new Vector3(Mathf.Cos((x / i) * Mathf.PI * 2) * 1.5f, 2 - ((float)i / 6), Mathf.Sin((x / i) * Mathf.PI * 2) * 1.5f);
                index++;
            }
        }
    }

    private void makecircle()
    {
        for (int i = 0; i < rocks.Count; i++)
        {
            rocks[i].transform.position = Vector3.Lerp(rocks[i].transform.position, circlepos[i] + transform.position, rockTime);
        }
        if (rockTime < 1)
        {
            rockTime += Time.deltaTime;
        }
        else
        {
            rockTime = 1;
        }
    }

    public void endAnimation(int number)
    {
        if (number == 0)
        {
            jumpattackstart = false;
            anim.SetBool("attack", false);
        }
        if (number == 1)
        {
            hammer.GetComponent<BoxCollider>().enabled = false;
            anim.SetBool("standingAttack", false);
            GetComponent<Rigidbody>().isKinematic = false;
        }
        if (number == 2)
        {
            anim.SetBool("slide", false);
        }
        if (number == 3)
        {
            anim.SetBool("throw", false);
        }
        if (number == 4)
        {
            hammer.GetComponent<BoxCollider>().enabled = false;
            anim.SetBool("down", false);
        }
        if (number == 5)
        {
            anim.SetBool("ragdolled", false);
        }
    }

    bool checkanim()
    {
        return anim.GetBool("attack") || anim.GetBool("slide") || anim.GetBool("standingAttack") || anim.GetBool("throw") || anim.GetBool("ragdolled");
    }

    private void chaingrab()
    {
        int maxrocks = 0;
        int maxlength = 0;
        if (rocks.Count > 10)
        {
            maxrocks = rocks.Count;
            if (maxrocks > 40)
            {
                maxrocks = 40;
            }
            if (maxrocks > 19)
            {
                maxlength = 19;
            }
            else
            {
                maxlength = maxrocks;
            }
        }
        

        if (Mathf.FloorToInt(time) >= maxlength)
        {
            reverse = -1;
            fasterAtStart = -.5f;
        }
        chainpos = new Vector3[20];
        chainpos[0] = new Vector3(transform.position.x + xslope * .5f, transform.position.y + 1, transform.position.z + zslope * .5f);
        chainpos[1] = new Vector3(transform.position.x + xslope * 1, transform.position.y + 1.5f, transform.position.z + zslope * 1);
        chainpos[2] = new Vector3(transform.position.x + xslope * 2, transform.position.y + 1.5f, transform.position.z + zslope * 2);
        for (int i = 3; i < 20; i++)
        {
            chainpos[i] = new Vector3(transform.position.x + xslope * i, transform.position.y + 1,transform.position.z + zslope * i);
        }
       
        
        for (int i = 0; i < maxrocks; i++)
        {

            if (i + Mathf.FloorToInt(time) >= maxrocks)
            {
                rocks[i].transform.position = Vector3.Lerp(chainpos[i + Mathf.FloorToInt(time) - maxrocks], chainpos[i + 1 + Mathf.FloorToInt(time) - maxrocks], time - Mathf.FloorToInt(time));
            }
            else if (i + Mathf.FloorToInt(time) + 1 >= maxrocks)
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, chainpos[i + 1 + Mathf.FloorToInt(time) - maxrocks], time - Mathf.FloorToInt(time));
            }
            else
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, rocks[i + 1 + Mathf.FloorToInt(time)].transform.position, time - Mathf.FloorToInt(time));
            }
        }
        
           
        if (Mathf.FloorToInt(time) < 3 && grabedenemy != null)
        {
            reverse = 0;
            fasterAtStart = 0;
            chiainAttackend = true;
            grabedenemy.transform.parent.position = new Vector3((transform.forward + transform.position).x, grabedenemy.transform.parent.position.y, (transform.forward + transform.position).z);
            
        }
        else
        {
             time += Time.deltaTime * 20 * reverse + fasterAtStart;
            fasterAtStart += -reverse * .02f;
        }
        if (Mathf.FloorToInt(time) < 0)
        {
            if (rocks[rocks.Count - 1].GetComponent<firstrock>() != null)
            {
                Destroy(rocks[rocks.Count - 1].GetComponent<firstrock>());
            }
                chainattack = false;
            time = 0;
            reverse = 1;
            fasterAtStart = .5f;
        }
        
    }

    public void chainfinish()
    {
       
        chainposfin = new Vector3[11];
        int index = 0;
        for (float i = -2; i < 2; i += .4f)
        {
            if (i < 0)
            {
                chainposfin[index] = transform.forward * Mathf.Abs(i) + transform.position;

            }
            else
            {
                chainposfin[index] = transform.forward * Mathf.Abs(i + 2)  + transform.position;
            }
            chainposfin[index].y = -Mathf.Pow(i,2)+3 + transform.position.y;
            
            index++;
        }
        if (time > 5 && rotated == false)
        {
            if (grabedenemy.tag == "itemr")
            {
                grabedenemy.tag = "grabbedr";
            }
            else
            {
                grabedenemy.tag = "grabbed";
            }
            rotated = true;
            grabedenemy.transform.Rotate(90, 0, 0);
            fasterAtStart = .3f;
        }
        int maxrocks = 0;
        int maxlength = 0;
        if (rocks.Count > 10)
        {
            maxrocks = rocks.Count;
            if (maxrocks > 40)
            {
                maxrocks = 40;
            }
            if (maxrocks > 19)
            {
                maxlength = 19;
            }
            else
            {
                maxlength = maxrocks;
            }
        }

        if (Mathf.FloorToInt(time) >= 10)
        {
            reverse = -1;
            fasterAtStart = 0f;
            Destroy(grabedenemy, 2f);
            if (grabedenemy != null)
            {
                grabedenemy.transform.parent = null;
                grabedenemy.transform.position = chainposfin[10];
            }
            
        }

        for (int i = 0; i < maxrocks; i++)
        {

            if (i + Mathf.FloorToInt(time) >= maxrocks)
            {
                rocks[i].transform.position = Vector3.Lerp(chainposfin[i + Mathf.FloorToInt(time) - maxrocks], chainposfin[i + 1 + Mathf.FloorToInt(time) - maxrocks], time - Mathf.FloorToInt(time));
            }
            else if (i + Mathf.FloorToInt(time) + 1 >= maxrocks)
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, chainposfin[i + 1 + Mathf.FloorToInt(time) - maxrocks], time - Mathf.FloorToInt(time));
            }
            else
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, rocks[i + 1 + Mathf.FloorToInt(time)].transform.position, time - Mathf.FloorToInt(time));
            }
        }

        time += Time.deltaTime * 5 * reverse + fasterAtStart;
        Debug.Log(fasterAtStart);

        if (Mathf.FloorToInt(time) < 0)
        {
            
            chiainAttackend = false;
            rotated = false;
            chainattack = false;
            chainfin = false;
            grabedenemy = null;
            time = 0;
            reverse = 1;
            fasterAtStart = .5f;
        }
    }

    public void dodgeback(Vector3 dir)
    {
        
        Vector3[] dodgepos = new Vector3[8];
        Vector3[] dodgepos2 = new Vector3[8];
        float height = 1;
        dir = -transform.forward;
        for (int i = 0; i < 8; i++)
        {
            dodgepos[i] = transform.position + new Vector3(0, height, 0) + dir * i*.5f + transform.right;
            height -= .125f;
        }

        height = 1;
        for (int i = 0; i < 8; i++)
        {
            dodgepos2[i] = transform.position + new Vector3(0, height, 0) + dir * i * .5f - transform.right;
            height -= .125f;
        }

        if (Mathf.FloorToInt(time) >= 8)
        {
           
            time = 7.9f;
            reverse = -1;
        }

        moverocks(dodgepos, 8 ,0);
        moverocks(dodgepos2, 16, 9);
        time += Time.deltaTime * 50 * reverse;
        if (Mathf.FloorToInt(time) < 0)
        {
            anim.SetBool("slide", false);
            dodge = false;
            time = 0;
            reverse = 1;
        }
        transform.Translate(Vector3.forward * 10* Time.deltaTime);
    }

    void moverocks(Vector3[] pos, int max ,int min)
    {
        for (int i = min; i < max; i++)
        {

            if (i + Mathf.FloorToInt(time) >= max)
            {
                rocks[i].transform.position = Vector3.Lerp(pos[i + Mathf.FloorToInt(time) - max], pos[i + 1 + Mathf.FloorToInt(time) - max], time - Mathf.FloorToInt(time));
            }
            else if (i + Mathf.FloorToInt(time) + 1 >= max)
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, pos[i + 1 + Mathf.FloorToInt(time) - max], time - Mathf.FloorToInt(time));
            }
            else
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, rocks[i + 1 + Mathf.FloorToInt(time)].transform.position, time - Mathf.FloorToInt(time));
            }
        }
    }

    public void sheild()
    {
        Debug.Log("sheild");
        Vector3[] sheildpos = new Vector3[25];
        sheildpos[0] = transform.position + transform.forward*2;
        Vector3 left = Vector3.Cross(transform.forward, Vector3.up).normalized;
        for (int i = 0; i < 5; i++)
        {
            for (int x = 0; x < 5; x++)
            {
                sheildpos[i * 5 + x] = transform.position + transform.forward * 2 + left * .5f * (i-2) + Vector3.up *.5f * x ;
            }
        }

        for (int i = 0; i < 25; i++)
        {
            rocks[i].transform.position = Vector3.Lerp(rocks[i].transform.position,sheildpos[i],time);
        }
        if (time < 1 || reverse == -1)
        {
            time += reverse * Time.deltaTime*3;
        }
        if (time < 0)
        {
            sheilds = false;
        }
    }

  

    public void jump()
    {
        Vector3[] jumpposR = new Vector3[11];
        int index = 0;
        float c = 0;
        for (float i = -2; i < 2; i += .4f)
        {
            if (i < 0)
            {
                jumpposR[index] = -transform.right * c + transform.position;
                jumpposR[index].y = -Mathf.Pow(i, 2) + 4;
            }
            else
            {
                jumpposR[index] = -transform.right * c + transform.position;
                jumpposR[index].y = -Mathf.Pow(i, 2);
            }

            c += .4f;
            index++;
        }
        c = 0;
        Vector3[] jumpposL = new Vector3[11];
        index = 0;
        for (float i = -2; i < 2; i += .4f)
        {
            if (i < 0)
            {
                jumpposL[index] = transform.right * c + transform.position;
                jumpposL[index].y = -Mathf.Pow(i, 2) + 4;

            }
            else
            {
                jumpposL[index] = transform.right * c + transform.position;
                jumpposL[index].y = -Mathf.Pow(i, 2);
            }
            c += .4f;

            index++;
        }

        if (Mathf.FloorToInt(time) >= 10)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up*10000);
            reverse = -1;
            fasterAtStart = 0f;
        }


        for (int i = 0; i < 11; i++)
        {

            if (i + Mathf.FloorToInt(time) >= 11)
            {
                rocks[i].transform.position = Vector3.Lerp(jumpposR[i + Mathf.FloorToInt(time) - 11], jumpposR[i + 1 + Mathf.FloorToInt(time) - 11], time - Mathf.FloorToInt(time));
            }
            else if (i + Mathf.FloorToInt(time) + 1 >= 11)
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, jumpposR[i + 1 + Mathf.FloorToInt(time) - 11], time - Mathf.FloorToInt(time));
            }
            else
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, rocks[i + 1 + Mathf.FloorToInt(time)].transform.position, time - Mathf.FloorToInt(time));
            }
        }

        for (int i = 11; i < 22; i++)
        {

            if (i + Mathf.FloorToInt(time) >= 22)
            {
                rocks[i].transform.position = Vector3.Lerp(jumpposL[i + Mathf.FloorToInt(time) - 22], jumpposL[i + 1 + Mathf.FloorToInt(time) - 22], time - Mathf.FloorToInt(time));
            }
            else if (i + Mathf.FloorToInt(time) + 1 >= 22)
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, jumpposL[i + 1 + Mathf.FloorToInt(time) - 22], time - Mathf.FloorToInt(time));
            }
            else
            {
                rocks[i].transform.position = Vector3.Lerp(rocks[i + Mathf.FloorToInt(time)].transform.position, rocks[i + 1 + Mathf.FloorToInt(time)].transform.position, time - Mathf.FloorToInt(time));
            }
        }

        time += Time.deltaTime * 5 * reverse + fasterAtStart;

        if (Mathf.FloorToInt(time) < 0)
        {
            jumping = false;
            time = 0;
            reverse = 1;
            fasterAtStart = .5f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "arm")
        {
            other.enabled = false;
            health -= 20;
        }
        if (other.tag == "spear")
        {
            health -= 30;
        }
        if (other.tag == "bigrock")
        {
            other.tag = "def";
            health -= 50;
        }
    }
    }

