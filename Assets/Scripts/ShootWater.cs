using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWater : MonoBehaviour
{
    GameObject prefab;
    public float speed;
    public Transform gun;
    private float randX;
    public ParticleSystem water;
    private float maxRot = 30f;
    private float minRot = 350f;
    private bool canShoot;
    public GameObject rotator;
    public Transform hand;
    Collider coll;
    Vector3 offset;
    Quaternion rot;

    void OnEnable()
    {
        rot = Quaternion.Euler(0, -90, 0);
        canShoot = true;
        offset = new Vector3(0, 10, 0); 
        coll = GetComponent<Collider>();
        coll.enabled = false;
        (rotator.GetComponent<RotatorGun>()).enabled = false;
        transform.localRotation = rot;
        transform.position = Vector3.zero; 
        transform.position = hand.transform.position;
        //transform.position = new Vector3(0, 0, 0);
        canShoot = true;
        prefab = Resources.Load("WaterBullet") as GameObject;
    }

    void Update()
    {
        IEnumerator FireRate()
        {
            canShoot = false;
            //start location bullet
            Shoot();
            yield return new WaitForSeconds(.05f);
            canShoot = true;
        }


        if (Input.GetButton("Fire1") && canShoot)
        {
            StartCoroutine(FireRate());
            water.Play();
        }
        
        float x = Input.GetAxis("Mouse Y") * 10;
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

    void Shoot()
        {
            GameObject bullet = Instantiate(prefab) as GameObject;
            bullet.transform.position = gun.transform.position; //+ Camera.main.transform.forward * 3;
            //shooting the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            randX = Random.Range(0.75f, 1);
            rb.velocity = offset + Camera.main.transform.forward * randX * speed;
        }

    
}