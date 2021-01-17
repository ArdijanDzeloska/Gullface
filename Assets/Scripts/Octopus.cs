using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    DocksPlayerController player;
    private bool shoot;
    void Start()
    {
        player = GameObject.FindObjectOfType<DocksPlayerController>(); 
        shoot = true;
        //(bullet.GetComponent<InkBullet>()).enabled = true;
        //(bullet.GetComponent<InkCollider>()).enabled = true;
       
        //(bullet.GetComponent<InkBullet>()).enabled = false;
        //(bullet.GetComponent<InkCollider>()).enabled = false;
    }
    private void Update()
    {
        if (transform.position.z - player.transform.position.z < 28 && shoot)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            shoot = false;
        }
    }
}