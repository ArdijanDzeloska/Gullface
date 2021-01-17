using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    private Vector3 left;
    private Vector3 right;
    private float left_wall_pos;
    private float right_wall_pos;
    private string moving;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // test test
        left = new Vector3(-7, 0, 0);
        right = new Vector3(7, 0, 0);
        left_wall_pos = -7f;
        right_wall_pos = 7f;
        moving = "left";
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, 4 -0.1f * transform.position.x * transform.position.x, transform.position.z);

        if (moving == "left")
        {
            if (transform.position.x > left_wall_pos)
            {
                transform.Translate(left * Time.deltaTime, Space.World);
            }

            else
            {
                moving = "right";
                transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            }
        }

        if (moving == "right")
        {
            if (transform.position.x < right_wall_pos)
            {
                transform.Translate(right * Time.deltaTime, Space.World);
            }

            else
            {
                moving = "left";
                transform.Rotate(0.0f, -180.0f, 0.0f, Space.Self);
            }
        }

}
}