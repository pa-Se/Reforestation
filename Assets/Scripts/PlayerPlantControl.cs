using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class PlayerPlantControl : MonoBehaviour
{

    [Header("Plant Settings")]
    public bool infiniteSeedMode;
    public Seed[] seeds;
    public GameObject plantPrefab;
    public Transform plantHandPos;
    public AudioSource audioBlob;

    public Animator animator;


    public Terrain terrain;
    public PlayerMovement controller;

    Transform cam;

    int[] numSeedsByType;

    int activePlantIndex = 0;

    // Initialisiere Nummerntasten
    KeyCode[] numberKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    public event System.Action<string, int> onPlantTypeSwitched;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start()
    {
        terrain = FindObjectOfType<Terrain>();
        cam = Camera.main.transform;
        controller = FindObjectOfType<PlayerMovement>();

        //
        numSeedsByType = new int[seeds.Length];


    }

    //Update wird einmal pro Frame aufgerufen
    void Update()
    {
        HandlePlantInput();
    }

    //Zum Wechseln zwischen den verschiedenen Samen/Pflanzen und Werfen der Samen
    void HandlePlantInput()
    {
        int plantIndexOld = activePlantIndex;
        //Wechsel der Samen
        for (int i = 0; i < seeds.Length; i++)
        {
            if (Input.GetKeyDown(numberKeys[i]))
            {
                if (numSeedsByType[i] > 0 || infiniteSeedMode)
                {
                    activePlantIndex = (activePlantIndex == i) ? -1 : i; //Wenn die Pflanzenart bereits ausgew�hlt ist, exit plant-mode, sonst zu anderer Pflanzenart wechseln
                    if (activePlantIndex != -1 && onPlantTypeSwitched != null)
                    {
                        onPlantTypeSwitched(seeds[i].plantPrefab.plantName, numSeedsByType[i]);
                    }
                }
                break;
            }
        }

        bool plantModeBecameActiveThisFrame = activePlantIndex != -1 && plantIndexOld == -1;
        bool plantModeBecameInactiveThisFrame = activePlantIndex == -1 && plantIndexOld != -1;

        //Werfen
        if (activePlantIndex != -1 && Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = plantHandPos.position;
            float terrainHeight = terrain.SampleHeight(spawnPos);
            audioBlob.Play();

            if (spawnPos.y > terrainHeight)
            {
                var seed = Instantiate(seeds[activePlantIndex], spawnPos, plantHandPos.rotation);
                seed.Throw(5f);

                if (!infiniteSeedMode)
                {
                    numSeedsByType[activePlantIndex]--;
                    if (numSeedsByType[activePlantIndex] == 0)
                    {
                        activePlantIndex = -1;
                    }
                }
            }
        }
    }
}
