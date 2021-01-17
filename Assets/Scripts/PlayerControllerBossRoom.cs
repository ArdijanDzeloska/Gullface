using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerControllerBossRoom : MonoBehaviour
{
    public float moveSpeed;
    //public Rigidbody bean;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;
    private bool isJumping;
    public static bool isWalking;
    private bool leftWalk;
    private bool rightWalk;

    private int playerHP;
    private int maxHP;
    public Transform player;
    private Vector3 spawn;
    public HealthBar health;
    private bool isHit;
    public static bool dead;
    public Animator anim;

    //flash when hit
    public Renderer playerRenderer;
    private float flashCounter;
    private float flashLength = 0.1f;
    public Image blackScreen;
    //private bool isFadeTo;
    private bool isFadeFrom;
    public float fadeSpeed;
    public TextMeshProUGUI PlayerHPText;
    GameObject prefab;

    //sounds
    public AudioSource gillface_laugh;
    public AudioSource gillface_laugh2;

    // Start is called before the first frame update
    void Start()
    {
        leftWalk = false;
        rightWalk = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        isJumping = false;
        isHit = false;
        isWalking = false;
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b);
        isFadeFrom = true;
        
        prefab = Resources.Load("Red Bullet") as GameObject;
        spawn = player.transform.position;
        //bean = GetComponent<Rigidbody>();
        maxHP = 100;
        playerHP = maxHP;
        health.SetMaxHealth(maxHP);

        controller = GetComponent<CharacterController>();
    }




    // Update is called once per frame
    void Update()
    {
        //check hp
        PlayerHPText.text = "HP: " + playerHP.ToString();

        //set movement
        float yStore = moveDirection.y;
        //move at mouse direction
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        //normalizer limits values to a circle, so you cant go faster if pressing W + D at once
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            isJumping = false;
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                moveDirection.y = jumpForce;
            }
        }

        if (flashCounter <= 0)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            flashCounter = flashLength;
        }
        else
        {
            playerRenderer.enabled = true;
        }

        //if (isFadeTo)
        //{
        //    isFadeFrom = false;
        //    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.g, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        //    if (blackScreen.color.a == 1f)
        //    {
        //        isFadeTo = false;
        //        isFadeFrom = true;
        //    }
        //}

        if (isFadeFrom)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.g, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFrom = false;
            }
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey("w") | Input.GetKey("s"))
        {
            isWalking = true;
            leftWalk = false;
            rightWalk = false;
        }
        else if (Input.GetKey("a"))
        {
            leftWalk = true;
            isWalking = false;
            rightWalk = false;
        }
        else if (Input.GetKey("d"))
        {
            rightWalk = true;
            leftWalk = false;
            isWalking = false;
        }
        
        else
        {
            isWalking = false;
            rightWalk = false;
            leftWalk = false;

        }

        
        if (playerHP <= 0)
        {
            dead = true;
            //anim.SetBool("dead", dead);
            PlayerHPText.text = "";
            //playerRenderer.enabled = false;
            controller.enabled = false;
        }

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("rightWalk", rightWalk);
        anim.SetBool("leftWalk", leftWalk);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isHit", isHit);
        anim.SetBool("dead", dead);
        isHit = false;

        if (dead)
        {
            StartCoroutine(End());
        }

    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
        }
    }

    public void UpdateHP(int difference)
    {
        playerHP = playerHP + difference;
    }

        private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Red Bullet") && dead == false)
        {
            gillface_laugh.Play();
            isHit = true;
            //anim.SetBool("isHit", isHit);
            Destroy(other.gameObject);
            UpdateHP(-10);
            health.SetHealth(playerHP);
            //Debug.Log(playerHP);
        }

        if (other.gameObject.CompareTag("Rock") && dead == false)
        {
            gillface_laugh2.Play();
            isHit = true;
            //anim.SetBool("isHit", isHit);
            Destroy(other.gameObject);
            UpdateHP(-20);
            health.SetHealth(playerHP);
            //Debug.Log(playerHP);
        }
        //if (playerHP <= 0)
        //{

        //    dead = true;
        //    //anim.SetBool("dead", dead);
        //    PlayerHPText.text = "";
        //    //playerRenderer.enabled = false;
        //    controller.enabled = false;
        //    StartCoroutine(End());
        //}
    }
}
