using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;

    public float groundDistance = 0.4f;
    public float jumpHeight = 6f;
    public LayerMask groundMask;
    public float speed = 12f;
    public float gravity = -9.81f;

    public Vector3 move;

    public float smoothMoveTime;
    public Vector3 velocity;
    Vector3 smoothV;

    public Vector3 throw_force;

    public bool isGrounded;


    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal"); // A = -1 , D = 1
        float z = Input.GetAxis("Vertical"); // W = 1 , S = -1

        //WASD-Movement
        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 20f;
        }
        else
        {
            speed = 12f;
        }


        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
