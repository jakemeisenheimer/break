using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backcontrol : MonoBehaviour
{
    public bool reverse = false;
    public float lerp = 0;
    public GameObject[] reset;
    public Vector3[] resetpos;
    public GameObject[] dis;
    public GameObject wall;
    public GameObject cam1;
    public GameObject cam2;
    bool end = false;
    void Start()
    {
        int i = 0;
        foreach (GameObject g in reset)
        {
            resetpos[i] = reset[i].transform.position;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            if (lerp > 1)
            {
                reverse = false;
                lerp = 0;
            }
            if (reverse)
            {
                lerp += Time.deltaTime;
            }

            bool actives = true;
            foreach (GameObject g in dis)
            {
                Debug.Log(g.activeSelf);
                actives = g.activeSelf & actives;
            }
            if (actives)
            {
                cam1.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<playerCon>().seccamera = cam2;
                foreach (MeshCollider b in wall.GetComponentsInChildren<MeshCollider>())
                {
                    b.enabled = true;
                }
                foreach (BoxCollider b in GetComponentsInChildren<BoxCollider>())
                {
                    b.enabled = true;
                }
                foreach (Rigidbody b in wall.GetComponentsInChildren<Rigidbody>())
                {
                    b.isKinematic = false;
                }
                foreach (Rigidbody b in GetComponentsInChildren<Rigidbody>())
                {
                    b.useGravity = true;
                }
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<BoxCollider>().enabled = false;
                end = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.R) && other.tag == "Player")
        {
            int i = 0;
            foreach (GameObject g in reset)
            {
                reset[i].SetActive(true);
                reset[i].transform.position = resetpos[i];
                i++;
            }
            i = 0;
            foreach (GameObject g in dis)
            {
                dis[i].SetActive(false);
                i++;
            }
            reverse = true;
        }
    }
}
