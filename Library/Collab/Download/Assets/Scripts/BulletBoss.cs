using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public float speed;
    public float addZ;
    Rigidbody rb;
    PlayerControllerBossRoom player;
    Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<PlayerControllerBossRoom>();
        //set direction of bullet
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y + 1, moveDirection.z + addZ);
        Destroy(gameObject, 2f);

        ////shooting the bullet
        //Rigidbody rb = bullet.GetComponent<Rigidbody>();
        //rb.velocity = Camera.main.transform.up + Camera.main.transform.forward * speed;

    }



    void OnTriggerEnter3D(Collider col)
    {
        if (col.gameObject.name.Equals("PlayerController2"))
        {
            Debug.Log("Hit!");
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
