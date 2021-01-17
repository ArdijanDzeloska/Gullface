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

    private int playerHP;
    public Transform player;
    private Vector3 spawn;
    //flash when hit
    public Renderer playerRenderer;
    private float flashCounter;
    private float flashLength = 0.1f;
    public Image blackScreen;
    private bool isFadeTo;
    private bool isFadeFrom;
    public float fadeSpeed;

    private bool isRespawning;
    //public Animator anim;
    public TextMeshProUGUI PlayerHPText;
    GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("Red Bullet") as GameObject;
        spawn = player.transform.position;
        //bean = GetComponent<Rigidbody>();
        playerHP = 100;
        controller = GetComponent<CharacterController>();
    }




    // Update is called once per frame
    void Update()
    {
        //check hp
        PlayerHPText.text = "HP: " + playerHP.ToString();
        if (playerHP <= 0)
        {
            Debug.Log('d');
            PlayerHPText.text = "";
            Respawn();
        }

        //set movement
        float yStore = moveDirection.y;
        //move at mouse direction
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        //normalizer limits values to a circle, so you cant go faster if pressing W + D at once
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
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

        if (isFadeTo)
        {
            isFadeFrom = false;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.g, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                isFadeTo = false;
                isFadeFrom = true;
            }
        }
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

        //animations
        //anim.SetBool("Grounded", controller.isGrounded);
        //anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }


    //void OnTriggerEnter3D(Collider col)
    //{
    //    if (col.gameObject.name.Equals(prefab))
    //    {
    //        Debug.Log("Hit!");
    //        Destroy(gameObject);
    //    }

    //}
    public void Respawn()
    {
        GameObject player = GameObject.Find("Player");
        CharacterController charController = player.GetComponent<CharacterController>();

        isFadeTo = true;
        new WaitForSeconds(3f);
        isFadeFrom = true;
        gameObject.SetActive(false); 
        charController.enabled = false;
        player.transform.position = spawn;
        playerHP = 100;
        charController.enabled = true;
        gameObject.SetActive(true);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Red Bullet"))
        {
            playerHP = playerHP - 20;
            Debug.Log(playerHP);

            if (playerHP <= 0)
            {
                Destroy(other.gameObject);
                PlayerHPText.text = "";
                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
        }

    }
}
