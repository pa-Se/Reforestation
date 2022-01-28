using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class Seed : MonoBehaviour
{
    public Vector2 throwForceMinMax = new Vector2(4.5f, 10f);
    public const float gravity = 10; //je höher, desto größer Schwerkraft

    public Plant plantPrefab;
    Vector3 velocity;

    Terrain terrain;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start()
    {
        //Zuweisung Terrain
        terrain = FindObjectOfType<Terrain>();
    }


    //Wenn Samen geworfen werden soll...
    public void Throw(float inheritedForce)
    {
        //skaliere den Samen
        this.transform.localScale = Vector3.one * Random.Range(.1f, 0.3f);
        //werfe ihn nach vorn
        velocity = transform.forward * (Random.Range(throwForceMinMax.x, throwForceMinMax.y) + inheritedForce);
    }

    //Update wird einmal pro Frame aufgerufen
    void Update()
    {
        //Wurfweite
        velocity -= Vector3.up * gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        //GetTerrainHeight am Punkt wo Samen auftrifft
        float terrainHeight = terrain.SampleHeight(transform.position);

        if (transform.position.y < terrainHeight)
        {
            //Drehung der Pflanze festlegen
            float angle = Random.value * Mathf.PI * 10;
            var plantRot = Quaternion.LookRotation(new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)), new Vector3());

            //Pflanzenprefab instanziieren
            Instantiate(plantPrefab, new Vector3(transform.position.x, terrainHeight, transform.position.z), plantRot);

            //Samen zerstören
            Destroy(gameObject);
        }
    }
}
