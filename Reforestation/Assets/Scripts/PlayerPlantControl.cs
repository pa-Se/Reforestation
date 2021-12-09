using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantControl : MonoBehaviour
{

    [Header("Plant Settings")]
    public bool infiniteSeedMode;
    public bool debug_autoPlantSeeds;

    public Seed[] seeds;
    public GameObject plantPrefab;
    public Transform plantHandPos;

    public Animator animator;


    public Terrain terrain;
    public PlayerMovement controller;

    Transform cam;

    int[] numSeedsByType;

    int activePlantIndex = 0;

    KeyCode[] numberKeys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
    public event System.Action<string, int> onPlantTypeSwitched;

    void Start()
    {
        terrain = FindObjectOfType<Terrain>();
        cam = Camera.main.transform;
        controller = FindObjectOfType<PlayerMovement>();

        if (!Application.isEditor)
        {
            //infiniteSeedMode = false;
        }

        numSeedsByType = new int[seeds.Length];

        // TEST:
        if (debug_autoPlantSeeds)
        {
            int n = 0;
            for (int i = 0; i < 100; i++)
            {
                if (n > 30)
                {
                    break;
                }
                Vector3 spawnPos = Random.insideUnitSphere * 20;
                //float terrainHeight = terrain.terrainData.GetHeight((int)spawnPos.x, (int)spawnPos.z);
                float terrainHeight = terrain.SampleHeight(spawnPos);
                if (spawnPos.y > terrainHeight)
                {
                    n++;
                    Instantiate(seeds[0], spawnPos, plantHandPos.rotation);
                }
            }
        }
    }

    void Update()
    {

        HandlePlantInput();

    }

    void HandlePlantInput()
    {
        int plantIndexOld = activePlantIndex;
        // Switch plant type:
        for (int i = 0; i < seeds.Length; i++)
        {
            if (Input.GetKeyDown(numberKeys[i]))
            {
                if (numSeedsByType[i] > 0 || infiniteSeedMode)
                {
                    activePlantIndex = (activePlantIndex == i) ? -1 : i; // if already in this slot exit plant mode, otherwise switch
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

        if (activePlantIndex != -1 && Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = plantHandPos.position;
            float terrainHeight = terrain.SampleHeight(spawnPos);

            if (spawnPos.y > terrainHeight)
            {
                var seed = Instantiate(seeds[activePlantIndex], spawnPos, plantHandPos.rotation);
                //animator.SetTrigger("Throw");
                seed.Throw(2f);
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
