using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;
    private float maxRot = 30f;
    private float minRot = 300f;


    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues)
        {
            offset = player.position - transform.position;
        }
        pivot.transform.position = player.transform.position;
        pivot.transform.parent = player.transform;

        //hide mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    // LateUpdate is called once per frame, after other updates
    void LateUpdate()
    {
        //X rotation
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        player.Rotate(0, horizontal, 0);

        //Y rotation w pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        //limit rotation
        if (pivot.rotation.eulerAngles.x > maxRot && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.localRotation = Quaternion.Euler(maxRot, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < minRot)
        {
            pivot.localRotation = Quaternion.Euler(minRot, 0, 0);
        }

        //move camera based on current rotation of player and offset
        float desiredYAngle = player.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = player.position - (rotation * offset);

        //transform.position = player.position - offset;
        
        if (transform.position.y < player.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y - 0.5f, transform.position.z);
        }
        
        transform.LookAt(player);

    }
}
