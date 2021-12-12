using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public enum PlantType { Flower, Tree }
    public PlantType plantType;

    public string plantName;
    public MeshRenderer stem;

    public float growTime = 2;
    public float flowerGrowTime = 1;



    public Transform[] flowers;
    public float[] flowerStartGrowTime;





    public Material stemMat;
    public float growthPercent;
    bool growing;



    void Start()
    {
        stemMat = stem.material;

        growing = true;

        stemMat.SetFloat("_GrowthPercent", 0);


        //Scale Size of GameObject
        transform.localScale = Vector3.one * Random.Range(0.2f, .3f);

        for (int i = 0; i < flowers.Length; i++)
        {
            flowers[i].localScale = Vector3.zero;
        }


    }

    void Update()
    {
        if (growing)
        {
            growthPercent += Time.deltaTime / growTime;
            stemMat.SetFloat("_GrowthPercent", growthPercent);

            // stop growing once growthPercent exceeds 1 (unless flowers are still growing -- handled in loop)
            growing = growthPercent < 1;
            for (int i = 0; i < flowers.Length; i++)
            {
                if (growthPercent > flowerStartGrowTime[i])
                {
                    flowers[i].localScale = Vector3.MoveTowards(flowers[i].localScale, Vector3.one, Time.deltaTime / flowerGrowTime);
                    if (flowers[i].localScale != Vector3.one)
                    {
                        growing = true;
                    }
                }
            }

            // Finished growing
            if (!growing)
            {
                FindObjectOfType<Garden>().AddPlant(this);
            }
        }
        else
        {
        }
    }

    [ContextMenu("Bake Flower Calculation")]
    public void PrecalculateFlowerGrowTimes()
    {
        // Get flower/leaf objects that should be 'grown' (scaled) as stem appears
        flowers = new Transform[stem.transform.childCount];
        for (int i = 0; i < flowers.Length; i++)
        {
            flowers[i] = stem.transform.GetChild(i);
            flowers[i].localScale = Vector3.one;
        }

        // Figure out where along stem the leaves/flowers appear
        flowerStartGrowTime = new float[flowers.Length];
        Mesh stemMesh = stem.gameObject.GetComponent<MeshFilter>().sharedMesh;
        for (int flowerIndex = 0; flowerIndex < flowers.Length; flowerIndex++)
        {
            Vector3 flowerPos = flowers[flowerIndex].position;
            float closestDst = float.MaxValue;
            int closestVertIndex = 0;
            for (int vertIndex = 0; vertIndex < stemMesh.vertices.Length; vertIndex++)
            {
                Vector3 vertWorld = stem.transform.TransformPoint(stemMesh.vertices[vertIndex]);
                float sqrDst = (vertWorld - flowerPos).sqrMagnitude;
                if (sqrDst < closestDst)
                {
                    closestDst = sqrDst;
                    closestVertIndex = vertIndex;
                }
            }
            float startGrowingTime = stemMesh.uv[closestVertIndex].y;
            flowerStartGrowTime[flowerIndex] = startGrowingTime;
        }

    }

}