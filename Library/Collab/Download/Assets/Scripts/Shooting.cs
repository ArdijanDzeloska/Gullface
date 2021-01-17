using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    GameObject prefab;
    public float speed;


    void Start()
    {
        prefab = Resources.Load("Bullet") as GameObject;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //start location bullet
            GameObject bullet = Instantiate(prefab) as GameObject;
            bullet.transform.position = transform.position + Camera.main.transform.forward * 3;
            //shooting the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.up + Camera.main.transform.forward * speed;
        }

    }

    
}