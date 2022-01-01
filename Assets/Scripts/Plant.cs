using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class Plant : MonoBehaviour {

    public enum PlantType { Flower, Tree }
    public PlantType plantType;

    public string plantName;


    public MeshRenderer stem;

    public float growTime = 2;
    public float flowerGrowTime = 1;

    public float scaleFactor;

    public Transform[] flowers;
    public float[] flowerStartGrowTime;

    public Material stemMat;
    public float growthPercent;
    public bool growing;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start(){
        stemMat = stem.material;

        growing = true;

        stemMat.SetFloat("_GrowthPercent", 0);

        //Skalieren der Pflanze
        transform.localScale = Vector3.one * Random.Range(0.2f, .3f);

        for (int i = 0; i < flowers.Length; i++) {
            flowers[i].localScale = Vector3.zero * scaleFactor;
        }
    }

    //Update wird einmal pro Frame aufgerufen
    void Update() {
        //Solange die Pflanze wächst...
        if (growing) {
            //Berechne um wieviel die Pflanze bereits gewachsen ist.
            growthPercent += Time.deltaTime / growTime;
            stemMat.SetFloat("_GrowthPercent", growthPercent);

            //Stoppe Pflanzenwachsum wenn growthPercent 1 überschreitet, ansonsten wächst sie weiter (growing = true).
            growing = growthPercent < 1;
            for (int i = 0; i < flowers.Length; i++) {
                if (growthPercent > flowerStartGrowTime[i]) {
                    flowers[i].localScale = Vector3.MoveTowards(flowers[i].localScale, Vector3.one, Time.deltaTime / flowerGrowTime);
                    if (flowers[i].localScale != Vector3.one)
                    {
                        growing = true;
                    }
                }
            }

            //Wenn sie mit dem Wachsen fertig ist und füge sie zum Garten hinzu.
            if (!growing) {
                FindObjectOfType<Garden>().AddPlant(this);
            }
        }
    }

    [ContextMenu("Bake Flower Calculation")]
    public void PrecalculateFlowerGrowTimes() {
        //Lade Blüten- bzw. Blätter-Objekte, die wachsen sollen bzw. skaliert werden müssen, sobald der Stiel erscheint
        flowers = new Transform[stem.transform.childCount];
        for (int i = 0; i < flowers.Length; i++) {
            flowers[i] = stem.transform.GetChild(i);
            flowers[i].localScale = Vector3.one;
        }

        //Berechne, wo am Stiel die Blüten oder Blätter während des Wachstums sitzen sollen
        flowerStartGrowTime = new float[flowers.Length];
        Mesh stemMesh = stem.gameObject.GetComponent<MeshFilter>().sharedMesh;
        for (int flowerIndex = 0; flowerIndex < flowers.Length; flowerIndex++) {
            Vector3 flowerPos = flowers[flowerIndex].position;
            float closestDst = float.MaxValue;
            int closestVertIndex = 0;
            for (int vertIndex = 0; vertIndex < stemMesh.vertices.Length; vertIndex++) {
                Vector3 vertWorld = stem.transform.TransformPoint(stemMesh.vertices[vertIndex]);
                float sqrDst = (vertWorld - flowerPos).sqrMagnitude;
                if (sqrDst < closestDst) {
                    closestDst = sqrDst;
                    closestVertIndex = vertIndex;
                }
            }
            float startGrowingTime = stemMesh.uv[closestVertIndex].y;
            flowerStartGrowTime[flowerIndex] = startGrowingTime;
        }
    }
}