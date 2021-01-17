using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFish : MonoBehaviour
{
    GameObject prefab;
    public float speed;
    public Transform gun;
    public GameObject rotator;
    public Transform hand;
    Collider coll;
    Quaternion rot;
    private bool canShoot;
    private void Start()
    {
        rot = Quaternion.Euler(0, -90, 0);
        coll = GetComponent<Collider>();
        coll.enabled = false;
        (rotator.GetComponent<RotatorGun>()).enabled = false;
        transform.localRotation = rot;
        transform.position = Vector3.zero;
        transform.position = hand.transform.position;
        prefab = Resources.Load("FishBullet") as GameObject;
    }
    void OnEnable()
    {
        canShoot = true;
        //transform.rotation = new Quaternion(0, 0, 45, 0);


    }

    // Update is called once per frame
    void Update()
    {
        IEnumerator FireRate()
        {
            canShoot = false;
            //start location bullet
            Shoot();
            yield return new WaitForSeconds(5f);
            canShoot = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            StartCoroutine(FireRate());
        }
        
    }
    void Shoot()
    {
        //start location bullet
        GameObject fish = Instantiate(prefab) as GameObject;
        fish.transform.position = gun.transform.position; //+ Camera.main.transform.forward * 3;
                                                          //shooting the bullet
        Rigidbody rb = fish.GetComponent<Rigidbody>();
        rb.velocity = Camera.main.transform.up + Camera.main.transform.forward * speed;
    }
}
