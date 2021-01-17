using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHeavy : MonoBehaviour
{
    GameObject prefab;
    public float speed;
    public Transform gun;
    private float maxRot = 30f;
    private float minRot = 330f;
    public GameObject rotator;
    public Transform hand;
    Collider coll;

    void Start()
    {
        coll = GetComponent<Collider>();
        coll.enabled = false;
        transform.position = Vector3.zero; 
        transform.position = hand.transform.position;
        (rotator.GetComponent<RotatorGun>()).enabled = false;
        //transform.position = new Vector3(0, 0, 0); 
        prefab = Resources.Load("HeavyBullet") as GameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //start location bullet
            GameObject bullet = Instantiate(prefab) as GameObject;
            bullet.transform.position = gun.transform.position; //+ Camera.main.transform.forward * 3;
            //shooting the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.up + Camera.main.transform.forward * speed;
        }

        float x = Input.GetAxis("Mouse Y") * 8;
        transform.Rotate(0, 0, x);

        if (transform.rotation.eulerAngles.z > maxRot && transform.rotation.eulerAngles.z < 180f)
        {
            transform.localRotation = Quaternion.Euler(0, -90, maxRot);
        }

        if (transform.rotation.eulerAngles.z < minRot && transform.rotation.eulerAngles.z > 180f)
        {
            transform.localRotation = Quaternion.Euler(0, -90, minRot);
        }
    }


    
}