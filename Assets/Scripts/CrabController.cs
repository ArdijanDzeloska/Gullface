﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    private Vector3 left;
    private Vector3 right;
    private float left_wall_pos;
    private float right_wall_pos;
    private string moving;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() 
    {
        // test test
        left = new Vector3(-5f, 0, 0);
        right = new Vector3(5f, 0, 0);
        left_wall_pos = -3f;
        right_wall_pos = 3f;
        moving = "left";
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (moving == "left")
        {
            if (transform.position.x > left_wall_pos)
            {
                transform.Translate(left * Time.deltaTime);
            }

            else
            {
                moving = "right";
            }
        }

        if (moving == "right")
        {
            if (transform.position.x < right_wall_pos)
            {
                transform.Translate(right * Time.deltaTime);
            }
            else
            {
                moving = "left";
            }
        }

    }


}
