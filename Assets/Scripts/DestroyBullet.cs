using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    //gameObject bullet;
    // Start is called before the first frame update
    void OnTriggerEnter3D(Collider col)
    {
        if ((gameObject.name.Equals("WaterBullet")) | (gameObject.name.Equals("FishBullet")) | (gameObject.name.Equals("HeavyBullet")))
        {
            return;
        }

        if ((gameObject.name.Equals("Red Bullet")))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
