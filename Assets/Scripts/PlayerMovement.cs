using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class PlayerMovement : MonoBehaviour {

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

    //Update wird einmal pro Frame aufgerufen
    void Update() {

        //Überprüfung, ob sich der Spieler auf dem Boden befindet
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        //Horizontale Bewegung mit A = -1, D = 1
        float x = Input.GetAxis("Horizontal");

        //Vertikale Bewegung mit W = 1, S = -1
        float z = Input.GetAxis("Vertical");

        //WASD-Bewegung des Spielers
        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //Wenn Leertaste gedrückt ist und Spieler am Boden ist, springe
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Erhöhe Geschwindigkeit wenn Shift-Taste gedrückt wird und Spieler am Boden ist
        if (Input.GetKey("left shift") && isGrounded) {
            speed = 20f;
        }

        //sonst setze Normalgeschwindigkeit
        else {
            speed = 12f;
        }


        //Schwerkraft
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
