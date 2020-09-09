using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstrock : MonoBehaviour
{
    public playerCon player;

    void Update()
    {
        if (GetComponentInChildren<Transform>() != null)
        {
            GetComponentInChildren<Transform>().position = transform.position;
        }
    }
        void OnTriggerEnter(Collider collision)
    {


        if (collision.tag == "pot")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCon>();
            collision.transform.parent = transform;
            player.grabedenemy = collision.gameObject;
            player.reverse = -1;
            player.fasterAtStart = -.5f;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.tag = "item";
        }
            if (collision.tag == "enemy")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCon>();
            collision.transform.parent = transform;
            player.grabedenemy = collision.gameObject;
            player.reverse = -1;
            player.fasterAtStart = -.5f;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.GetComponent<Animator>().SetBool("grabbed",true);
            collision.gameObject.transform.LookAt(player.transform, Vector3.up);
            collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.GetComponent<zombieEnemy>().trueonRagdoll();
            collision.GetComponent<zombieEnemy>().noGravity();
            collision.GetComponent<zombieEnemy>().ragdolled = true;
            collision.GetComponent<zombieEnemy>().centerOfMass.GetComponent<Rigidbody>().isKinematic = true;
            collision.tag = "item";
        }
        if (collision.tag == "robot")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerCon>();
            collision.transform.parent = transform;
            player.grabedenemy = collision.gameObject;
            player.reverse = -1;
            player.fasterAtStart = -.5f;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            
            collision.gameObject.transform.LookAt(player.transform, Vector3.up);
            collision.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.GetComponent<robot>().trueonRagdoll();
            collision.GetComponent<Rigidbody>().useGravity = false;
            collision.GetComponent<robot>().ragdolled = true;
            collision.GetComponent<robot>().breakcube();
            collision.GetComponent<robot>().centerofmass.GetComponent<Rigidbody>().isKinematic = true;
            collision.GetComponent<robot>().noGravity();
            collision.tag = "itemr";
        }

    }

}
