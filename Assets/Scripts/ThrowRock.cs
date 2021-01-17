using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    GameObject prefab;
    public float speed;
    public Transform rock;
    public float addZ;
    Rigidbody rb;
    PlayerControllerBossRoom getPlayer;
    Rigidbody player;
    Vector3 moveDirection;
    public float moveSpeed;
    public float explosionSpeed;
    public float radius;
    public float power;
    Vector3 add;
    private bool isDead;
    public static bool throwRock;
    public static bool walking;
    void Start()
    {
        add = new Vector3(0, 2, -3); 
        walking = false;
        throwRock = true;
        getPlayer = GameObject.FindObjectOfType<PlayerControllerBossRoom>();
        player = getPlayer.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>(); 
        StartCoroutine("Timer");
        //prefab = Resources.Load("Rock") as GameObject;
    }

    //explosion when hit
    private void OnCollisionEnter(Collision other)
    {
        
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb == player)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3f);
            }
        }
    }

    IEnumerator Timer()
    {
        if (!isDead)
        {
            //Debug.Log("Throw");//spawn a new bullet
            yield return new WaitForSeconds(3f);
            Throw();
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }
    void Throw()
    {
        //set direction of bullet
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y + 10, moveDirection.z + addZ);
        throwRock = false;
        walking = true;
    }

    // Update is called once per frame
    void Update()
    {
        isDead = Enemy.dead;
    }
}
