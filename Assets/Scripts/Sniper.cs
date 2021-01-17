using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public GameObject bullet;
    public GameObject rock;
    public float fireRate;
    public float waitTime;
    public float switchTime;
    private Vector3 add;
    public Animator anim;
    PlayerControllerBossRoom player;
    public Transform playerPos;
    private bool shoot;
    public static bool throwRock;
    private bool bossDead;
    private bool playerDead;
    public static bool walking;
    private float meleeTime;
    public float timeValue;
    private bool start;
    private bool melee;
    private bool canWalk;
    public static bool meleeDamage;

    //sounds
    public AudioSource gillface_attack;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerControllerBossRoom>(); 
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        Begin();
    }

    void Begin()
    {
        meleeDamage = false; 
        walking = true;
        //num = 0;
        shoot = false;
        melee = false;
        throwRock = false;
        fireRate = 2f;
        switchTime = 5f;
        waitTime = Time.time;
        add = transform.forward * -3f + transform.up*2f + transform.right*0f;

        start = true;
    }

    void Update()
    {
        if (start)
        {
            meleeDamage = false;
            bossDead = Enemy.dead;
            playerDead = PlayerControllerBossRoom.dead;
            
            //check distance player gillface, if close do melee attack. Else run functions
            float x = playerPos.transform.position.x - transform.position.x;
            float z = playerPos.transform.position.z - transform.position.z;

            if ((x * x + z * z) < 20)
            {
                melee = true;
                canWalk = false;
                throwRock = false;
                if (PlayerControllerBossRoom.isWalking == false && Time.time > meleeTime)
                {
                    player.UpdateHP(-30);
                    meleeTime = Time.time + timeValue;
                    melee = false;
                }

            }
            else
            {
                melee = false;
                canWalk = true;
                CheckTime();
            }
            
            if (bossDead | playerDead)
            {
                TurnOff();
            }

            anim.SetBool("shoot", shoot);
            anim.SetBool("throwRock", throwRock);
            anim.SetBool("walking", walking);
            anim.SetBool("melee", melee);

        }

    }
    void TurnOff()
    {
        shoot = false;
        throwRock = false;
        walking = false;
        melee = false;
        switchTime = 100f;
    }

    void Random()
    {
        System.Random r = new System.Random();
        int randChoice = r.Next(5, 20);
        switchTime = Time.time + randChoice;
    }
    void Shoot()
    {
        if (Time.time > waitTime && throwRock == false && canWalk == true)
        {
            
            //spawn a new bullet
            //Debug.Log("Shoot"); 
            Instantiate(bullet, transform.position + add, Quaternion.identity);
            waitTime = Time.time + fireRate;
        }
    }

    //public void SetMove()
    //{
    //    walking = !walking;
    //}


    void Throw()
    {
        //Debug.Log("Throw");
        //first do animation, then throw the rock
        gillface_attack.Play();
        Instantiate(rock, transform.position, Quaternion.identity);
        Random();
        //walking = ThrowRock.walking;
        throwRock = ThrowRock.throwRock;
    }

    void CheckTime()
    {
        if (!melee)
        {
            //if time has passed, choose attack. Then check time for fire rate
            if (Time.time > switchTime && throwRock == false)
            {
                shoot = false;
                throwRock = true;
                walking = false;
                Throw();
                //num += 1;
                //if (num % 2 == 0)
                //{
                //    Shoot();
                //}
                //else
                //{
                //    Throw();
                //}

            }

            else
            {
                shoot = true;
                throwRock = false; 
                //Debug.Log("Shoot");
                Shoot();
            }
        }
    }

}