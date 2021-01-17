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
    private float minRot = 330f;
    private bool canShoot;
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
            rb.velocity = gun.transform.up + Camera.main.transform.forward * randX * speed;
        }

    
}