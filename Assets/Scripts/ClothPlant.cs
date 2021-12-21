using System.Threading;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothPlant : MonoBehaviour
{

    public SkinnedMeshRenderer mesh;
    public Plant plant;

    float blendOne = 100f;

    // Start is called before the first frame update
    void Start()
    {

        mesh.SetBlendShapeWeight(0, blendOne);



    }

    // Update is called once per frame
    void Update()
    {

        if (!plant.growing && blendOne > 5.0f)
        {
            mesh.SetBlendShapeWeight(0, blendOne);
            blendOne -= 0.025f;

        }



    }
}
