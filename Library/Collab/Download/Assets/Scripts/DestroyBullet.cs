using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    //gameObject bullet;
    // Start is called before the first frame update
    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
