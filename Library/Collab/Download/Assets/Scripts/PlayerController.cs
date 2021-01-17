using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
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

    private bool isRespawning;
    public Animator anim;
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

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);

        //animations
        anim.SetBool("Grounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
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

        charController.enabled = false;
        player.transform.position = spawn;
        playerHP = 100;
        charController.enabled = true;
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
            }
        }

    }
}
