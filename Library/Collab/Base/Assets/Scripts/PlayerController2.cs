using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController2 : MonoBehaviour
{
    public float speed;
    private float moveSpeed;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float movementZ;
    private int max_speed = 15;
    private int slow_time;
    private int launch_time;
    //invisibility stuff
    public float invisLength;
    private float invisCounter;
    public Renderer playerRenderer;
    private float flashCounter;
    private float flashLength = 0.1f;

    public AudioSource fountain_sound;
    public int hp;
    public TextMeshProUGUI hpText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = 100;
        moveSpeed = speed;
        SetHPText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
    }

    void SetHPText()
    {
        hpText.text = "HP: " + hp.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && transform.position.y < 2.5f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 7, rb.velocity.z);
        }
        if ((rb.velocity.z > max_speed) && (Time.time > launch_time))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, max_speed);
        }

        if (Time.time < slow_time)
        {
            speed = 5;
        }
        else
        {
            speed = moveSpeed;
        }

        if (invisCounter > 0)
        {
            invisCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }
            if (invisCounter <= 0)
            {
                playerRenderer.enabled = true;
            }

        }

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, 1);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("SlowDown"))
        {
            other.gameObject.SetActive(false);
            slow_time = (int)Time.time + 3;
            rb.velocity = new Vector3(rb.velocity.x / 10, rb.velocity.y / 10, rb.velocity.z / 10);
        }

        if (other.gameObject.CompareTag("Fountain") && (Time.time % 10 < 5))
        {
            rb.velocity = new Vector3(rb.velocity.x, 7, rb.velocity.z);
            fountain_sound.Play();
        }

        if (other.gameObject.CompareTag("Slide"))
        {
            launch_time = (int)Time.time + 1;
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, max_speed + 3);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            BounceOff();
        }

        if (other.gameObject.CompareTag("Crab"))
        {
            BounceOff();
            if (invisCounter <= 0)
                UpdateHP(-30);
                invisCounter = invisLength;
                playerRenderer.enabled = false;
                flashCounter = flashLength;
        }

        if (other.gameObject.CompareTag("Fish"))
        {
            BounceOff();
            if (invisCounter <= 0)
                UpdateHP(-20);
            invisCounter = invisLength;
            playerRenderer.enabled = false;
            flashCounter = flashLength;
        }
    }


    private void BounceOff()
    {
        rb.velocity = new Vector3(-rb.velocity.x, 0, -rb.velocity.z);
    }

    public void UpdateHP(int val)
    {
        hp = hp + val;
        SetHPText();
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}