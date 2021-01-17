using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.IO;
//using Unity.Mathematics;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float realspeed;
    //private Rigidbody rb;
    private Vector3 moveDirection;
    public bool isGrounded;
    public CharacterController controller;
    public GameObject player;
    public TextMeshProUGUI distanceToGillface;
    private float distance_to_gillface;
    private float nextLogTime;
    private float interval = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        realspeed = speed;
        //rb = GetComponent<Rigidbody>();
        WriteToLog2("docks has started -------------------------");
        nextLogTime = Time.time;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }



    void Update()
    {
        //normalizer limits values to a circle, so you cant go faster if pressing W + D at once;
        moveDirection.z = realspeed;
        controller.Move(moveDirection * Time.deltaTime);
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);

        distance_to_gillface = player.transform.position.z - transform.position.z;
        SetDistanceToGillfaceText(distance_to_gillface);

        //transform.position += transform.forward * 0.5f * realspeed * Time.deltaTime;

        if (distance_to_gillface > 8)
        {
            realspeed = realspeed + 0.4f * Time.deltaTime;
        }
        else
        {
            realspeed = speed;  //Mathf.MoveTowards(speed, 10f, 20f*Time.deltaTime);
        }

        if (GlobalScript.Distance + player.transform.position.z > 1000)
        {
            //Debug.Log("player has run 1000 meters");
            GlobalScript.Distance = GlobalScript.Distance + player.transform.position.z;
            UnityEngine.SceneManagement.SceneManager.LoadScene("BossRoom");
        }

        if (transform.position.z + 1 >= player.transform.position.z)
        {
            GlobalScript.Distance = GlobalScript.Distance + player.transform.position.z;
            if (GlobalScript.Lives > 0)
            {
                GlobalScript.Lives = GlobalScript.Lives - 1;
                //Debug.Log("gillface has catched up");
                //Debug.Log(GlobalScript.Distance);
                UnityEngine.SceneManagement.SceneManager.LoadScene("BossRoom");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
            }
        }
        

        if (Time.time > nextLogTime)
        {
            nextLogTime += interval;
            WriteToLog2(distance_to_gillface.ToString());
        }
    }

    void SetDistanceToGillfaceText(float dist)
    {
        dist = (float)System.Math.Round(dist, 1);
        distanceToGillface.text = "Distance to Gillface: " + dist.ToString();
    }

    void WriteToLog2(string content)
    {
        //string my_path = GlobalScript.Filepath2;
        //content = content + "    " + Time.time + "\n";
        //File.AppendAllText(my_path, content);
    }

}