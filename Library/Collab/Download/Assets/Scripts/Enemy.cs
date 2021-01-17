using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    private int hp;
    public TextMeshProUGUI HPText;
    public GameObject player;
    public HealthBar health;
    private bool isHit;
    public static bool walking;
    public static bool dead;
    public Animator anim;
    private float pathTime;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    protected NavMeshAgent Agent;
    NavMeshPath path;
    Sniper enemy;
    private bool stopWalk;
    private bool start;

    //sounds
    public AudioSource gillface_dying;
    public AudioSource gillface_taunt;
    public AudioSource gillface_roar;

    // Start is called before the first frame update
    void Start()
    {
        gillface_taunt.Play();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        Begin();
    }

    void Begin()
    {
        Debug.Log("start of cave");
        Debug.Log(GlobalScript.Distance);
        walking = true;
        Agent = GetComponent<NavMeshAgent>();
        enemy = GameObject.FindObjectOfType<Sniper>();
        isHit = false;
        dead = false;
        start = true;
        hp = 500;
        path = new NavMeshPath();
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            stopWalk = Sniper.throwRock;
            pathTime = Random.Range(1, 5);
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
            HPText.text = "HP: " + hp.ToString();
            if (hp <= 0)
            {
                HPText.text = "";
            }
            if (!inCoRoutine)
            {
                StartCoroutine(Move());
                //walking = false;
            }

            if (walking)
            {
                Agent.updateRotation = false;
                //enemy.SetMove();
            }
            if (stopWalk)
            {
                walking = false;
                Agent.speed = 0;

            }
            if (!stopWalk)
            {
                walking = true;
            }

            anim.SetBool("isHit", isHit);
            anim.SetBool("dead", dead);
            anim.SetBool("walking", walking);
            isHit = false;
        }
    }

    Vector3 getNewRandomPosition()
    // setting these ranges is vital larger seems better 
    {
        if (walking)
        {
            float x = player.transform.position.x + Random.Range(0, 8);
            //   float y = Random.Range(-20, 20);
            float z = player.transform.position.z + Random.Range(0, 8);

            Vector3 pos = new Vector3(x, 0, z);
            return pos;

        }
        else
        {
            Vector3 pos = new Vector3(0, 0, 0);
            return pos;
        }
        
    }

    void GetNewPath()
    {
        target = getNewRandomPosition();
        Agent.speed = Random.Range(2,8); 
        Agent.SetDestination(target);
        enemy.SetMove();
    }
    IEnumerator Move()
    {
        if (walking)
        {
            inCoRoutine = true;
            GetNewPath();
            validPath = Agent.CalculatePath(target, path);
            while (!validPath)
            {
                yield return new WaitForSeconds(0.01f);
                GetNewPath();
                validPath = Agent.CalculatePath(target, path);
            }
            yield return new WaitForSeconds(pathTime);
            inCoRoutine = false;
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(5);

        if (GlobalScript.Distance > 999)
        {
            Debug.Log("Gillface is dead");
            Debug.Log(GlobalScript.Distance);
            GlobalScript.Won = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Docks");
        }
    }
    public void UpdateHP(int difference)
    {
        hp = hp + difference;
        health.SetHealth(hp);
        if (hp <= 0)
        {
            dead = true;
            gillface_dying.Play();
            StartCoroutine(End());
            
        }
        else
        {
            if (difference < -15)
            {
                gillface_roar.Play();
            }
        }
    }

    public void SetHit()
    {
        isHit = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            UpdateHP(-20);
            SetHit();
        }

        if (other.gameObject.CompareTag("WaterBullet"))
        {
            Destroy(other.gameObject);
            UpdateHP(-1);
            SetHit();
        }

    }
}
