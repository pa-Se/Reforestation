using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 120f;
    public Transform playerBody;

    //Rotation um die X-Achse
    float xRotation = 20f;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start()
    {
        //Lockt den Cursor der Maus, sollte er offscreen sein. Wenn locked, dann befindet er sich im Zentrum des Views und kann nicht verschoben werden.
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Update wird einmal pro Frame aufgerufen
    void Update()
    {

        //horizontale Fortbewegung
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //vertikale Fortbewegung
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Bei jedem Frame wird die xRotation verringert, je nach derzeitiger vertikaler Fortbewegung.
        //-> Wert der Bewegung nach rechts oder links wird von der Rotation abgezogen, sollte sich der Spieler drehen.
        xRotation -= mouseY;

        //Verhindert, dass sich der Spieler bzw. die Kamera zu schnell um die eigene Achse dreht
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // (x,y,z)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
