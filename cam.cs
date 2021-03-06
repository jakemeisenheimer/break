﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform pos;
    public Transform player;
    public bool first = false;
    public float y;
    public float z = 0;
    Vector3 lastmousepos;
    Vector3 firstmousepos;
    Vector3 local;
    // Start is called before the first frame update
    void Start()
    {
        local = new Vector3(0, 5, -5);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position != pos.position || transform.rotation != pos.rotation) && first == false)
        {
           transform.position = Vector3.MoveTowards(transform.position, pos.position, .2f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, pos.rotation, 10);
        
        }

        if (Input.GetMouseButton(1))
        {
            if (lastmousepos == Vector3.zero)
            {
                lastmousepos = Input.mousePosition;
            }
            firstmousepos = (Input.mousePosition - lastmousepos) * .1f; ;

            lastmousepos = Input.mousePosition;

            if (firstmousepos.x > 0)
            {
                transform.RotateAround(player.transform.position, Vector3.up, firstmousepos.x);
                y += firstmousepos.x;
            }
            if (firstmousepos.x < 0)
            {
                transform.RotateAround(player.transform.position, Vector3.up, firstmousepos.x);
                y += firstmousepos.x;
            }
            if (firstmousepos.y < 0 && z > -30)
            {
                transform.RotateAround(player.transform.position, transform.right, -firstmousepos.y);
                z += firstmousepos.y;
            }
            if (firstmousepos.y > 0 && z < 30)
            {
                transform.RotateAround(player.transform.position, transform.right, -firstmousepos.y);
                z += firstmousepos.y;
            }
        }
        else
        {
            lastmousepos = Vector3.zero;
        }
    }

    void OnTriggerStay(Collider other)
    {
        
        transform.position = Vector3.MoveTowards(transform.position, player.position, .1f);
    }
    }
