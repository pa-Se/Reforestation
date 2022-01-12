using System.Threading;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class ClothPlant : MonoBehaviour {

    //wird benötigt um Animationen zu rendern, deren Objekte sich durch Gelenke verbiegen können
    public SkinnedMeshRenderer mesh;
    public Plant plant;

    float blendOne = 100f;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start() {
        mesh.SetBlendShapeWeight(0, blendOne);
    }

    //Update wird einmal pro Frame aufgerufen
    void Update() {
        //Wenn Pflanzenwachstum abgeschlossen ist, dann öffne die Blüte
        if (!plant.growing && blendOne > 5.0f) {
            mesh.SetBlendShapeWeight(0, blendOne);
            blendOne -= 0.025f;
        }
    }
}
