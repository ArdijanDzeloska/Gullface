using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float moveSpeed;
    public float explosionSpeed;
    public float radius;
    public float power;
    Enemy enemy;
    Rigidbody boss;
    public ParticleSystem smoke;

    void Start()
    {
        enemy = GameObject.FindObjectOfType<Enemy>();
        boss = enemy.GetComponent<Rigidbody>();
        StartCoroutine("Timer");

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb == boss)
            {
                enemy.UpdateHP(-50);
                enemy.SetHit();
                rb.AddExplosionForce(100f*power, explosionPos, radius, 2f);
                smoke.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
