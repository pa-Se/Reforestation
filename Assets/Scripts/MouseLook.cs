using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    //Rotation around x
    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        //Locks MouseCursor if Cursor is offscreen
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        //Every frame x-Rotation gets decreased based on Mouse-Y parameter
        xRotation -= mouseY;

        //Prevents overrotating the Camera

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // (x,y,z)
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
