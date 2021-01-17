using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBullet : MonoBehaviour
{
    [SerializeField]
    public float speed;
    Rigidbody bullet;
    Vector3 moveDirection;
    DocksPlayerController player;
    public Transform ink;

    void Start()
    {
        bullet = GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<DocksPlayerController>();
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        bullet.velocity = new Vector3(moveDirection.x, moveDirection.y + 1, 0);
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pier"))
        {
            gameObject.SetActive(false);
            Instantiate(ink, transform.position, ink.rotation);
        }

    }

}