using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.IO;


public class DocksPlayerController : MonoBehaviour
{
    public GameObject[] hearts;
    public Rigidbody rb;
    private float slow_time;
    private float ink_time;
    private float hit_time;
    private float bread_time;
    private float slow_factor;
    private float ink_factor;
    private float hit_factor;
    private float bread_factor;
    private float falling_term;
    private float stop_moving;
    private bool end;
    private bool IsRunning;
    private bool isJumping;
    private bool IsHit;

    //invisibility stuff
    //public float invisLength;
    //private float invisCounter;
    //public Renderer playerRenderer;
    //private float flashCounter;
    //private float flashLength = 0.1f;

    // sound and GUI
    public AudioSource fountain_sound;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI DistanceText;

    // data analytics
    private float nextLogTime;
    private float interval = 0.1f;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        isJumping = false;
        //IsRunning = true;
        rb = GetComponent<Rigidbody>();
        SetLivesText();
        SetDistanceText();
        end = false;
        nextLogTime = Time.time;
        WriteToLog("docks has started -------------------------");
        WriteToLog3("docks has started -------------------------");
        Debug.Log("Hello " + GlobalScript.Name);
        falling_term = 0f;
        stop_moving = 1f;
    }

    void WriteToLog(string content)
    {
        //string my_path = GlobalScript.Filepath;
        //content = content + "    " + Time.time + "\n";
        //File.AppendAllText(my_path, content);
    }

    void WriteToLog3(string content)
    {
        //string my_path = GlobalScript.Filepath3;
        //content = content + "    " + Time.time + "\n";
        //File.AppendAllText(my_path, content);
    }

    void SetLivesText()
    {
        LivesText.text = "Lives: " + GlobalScript.Lives.ToString();
    }

    void SetDistanceText()
    {
        float dist = GlobalScript.Distance + rb.position.z;
        float rounded_dist = (float)System.Math.Round(dist, 0);
        DistanceText.text = "Distance run: \n" + rounded_dist.ToString() + " / 1000";
    }


    

    void Update()
    {
        // make player jump
        if (Input.GetKeyDown("space") && transform.position.y < 3.5f)
        {
            //IsRunning = false;
            isJumping = true;
            rb.velocity = new Vector3(0, 7, 0);
        }

        // check if player recently picked up a turtle
        if (Time.time > slow_time)
        {
            slow_factor = 1f;
        }

        // check if player recently stepped on an ink stain
        if (Time.time > ink_time)
        {
            ink_factor = 1f;
        }

        if (Time.time > hit_time)
        {
            hit_factor = 1f;
        }

        if (Time.time > bread_time)
        {
            bread_factor = 1;
        }


        if (Time.time > nextLogTime)
        {
            nextLogTime += interval;
            WriteToLog3((GlobalScript.Distance + rb.position.z).ToString());
        }

        SetDistanceText();

        //if (GlobalScript.Distance + rb.position.z > 1000)
        //{
        //    Debug.Log("doing stuff in docksplayercontroller");
        //    GlobalScript.Distance = GlobalScript.Distance + rb.position.z;
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("BossRoom");
        //}



        anim.SetBool("isJumping", isJumping);
        anim.SetBool("IsHit", IsHit); 
        isJumping = false;
        IsHit = false;

        if (GlobalScript.Lives < 1)
        {
            Destroy(hearts[GlobalScript.Lives]);
        }
        else if (GlobalScript.Lives < 2)
        {
            Destroy(hearts[GlobalScript.Lives]);
        }
        else if (GlobalScript.Lives < 3)
        {
            Destroy(hearts[GlobalScript.Lives]);
        }
    }

    void FixedUpdate()
    {
        //Debug.Log(rb.position.y);
        if (!end)
        {
            rb.position = new Vector3(rb.position.x, rb.position.y - falling_term, rb.position.z + 14 * Time.deltaTime * slow_factor * ink_factor * hit_factor * bread_factor * stop_moving);

            // Player moves left
            if (Input.GetKey(KeyCode.A) && rb.position.x > -3f)
            {
                rb.position = new Vector3(rb.position.x - 6 * Time.deltaTime * slow_factor, rb.position.y, rb.position.z);
            }

            // Player moves right
            if (Input.GetKey(KeyCode.D) && rb.position.x < 3f)
            {
                rb.position = new Vector3(rb.position.x + 6 * Time.deltaTime * slow_factor, rb.position.y, rb.position.z);
            }

            if (rb.position.y < -10)
            {
                GlobalScript.Distance = GlobalScript.Distance + rb.position.z;
                if (GlobalScript.Lives > 0)
                {
                    GlobalScript.Lives = GlobalScript.Lives - 1;
                    Debug.Log(GlobalScript.Distance);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("BossRoom");
                }
                else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("SlowDown"))
        {
            other.gameObject.SetActive(false);
            WriteToLog("Picked up turtle");
            slow_factor = 0.5f;
            slow_time = (int)Time.time + 1.5f;
        }

        if (other.gameObject.CompareTag("Fountain"))
        {
            if (Time.time % 10 < 5)
            {
                rb.velocity = new Vector3(0, 10, 0);
                isJumping = true;
                WriteToLog("Launched by fountain");
                fountain_sound.Play();
            }

            else if (rb.position.y < 3.5)
            {
                falling_term = 0.1f;
                stop_moving = 0f;
            }
        }

        if (other.gameObject.CompareTag("Ink"))
        {
            WriteToLog("Slid over ink");
            ink_factor = 2f;
            ink_time = (int)Time.time + 1;
        }

        if (other.gameObject.CompareTag("Bread"))
        {
            other.gameObject.SetActive(false);
            System.Random rnd = new System.Random();
            int bread_rand = rnd.Next(1, 3);
            if (bread_rand == 1)
            {
                bread_factor = 2f;
                WriteToLog("Picked up healthy bread");
            }
            else if (bread_rand == 2)
            {
                WriteToLog("Picked up rotten bread");
                bread_factor = 0.5f;
            }
            bread_time = (int)Time.time + 1;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            BounceOff();
            WriteToLog("Hit by box");
        }

        if (other.gameObject.CompareTag("Crab"))
        {
            BounceOff();
            WriteToLog("Hit by crab");
            //CheckInvincible();
        }

        if (other.gameObject.CompareTag("Fish"))
        {
            BounceOff();
            WriteToLog("Hit by fish");
            //CheckInvincible();
        }
    }


    private void BounceOff()
    {
        IsHit = true;
        hit_factor = -0.5f;
        hit_time = (float)Time.time + 0.5f;
    }
}