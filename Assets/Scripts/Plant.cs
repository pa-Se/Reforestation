using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class Plant : MonoBehaviour
{
    public string plantName;
    public MeshRenderer stem;


    //Wachstumszeit für den Stamm
    public float growTime = 2;

    //Wachstumszeit für die Blätter/Blüten
    public float flowerGrowTime = 1;

    //Zum manuellen Anpassen der Skalierung des Objekts vom Inspektor aus
    public float scaleFactor;

    //Vektoren für jedes Blatt in einem Array
    public Transform[] flowers;

    //Wann soll Blatt anfangen mit wachsen?
    public float[] flowerStartGrowTime;

    //Material - Shader
    public Material stemMat;

    //Wachstumsfortschritt?
    public float growthPercent;

    //Wachstum abgeschlossen?
    public bool growing;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start()
    {
        //Initialisierung des Shader-Materials
        stemMat = stem.material;

        growing = true;

        stemMat.SetFloat("_GrowthPercent", 0);

        //Skalieren der Pflanze
        transform.localScale = Vector3.one * Random.Range(0.2f, .3f);

        for (int i = 0; i < flowers.Length; i++)
        {
            flowers[i].localScale = Vector3.zero * scaleFactor;
        }
    }

    //Update wird einmal pro Frame aufgerufen
    void Update()
    {
        //Solange die Pflanze wächst...
        if (growing)
        {
            //Berechne um wieviel die Pflanze bereits gewachsen ist, abhängig von der Zeit
            growthPercent += Time.deltaTime / growTime;
            stemMat.SetFloat("_GrowthPercent", growthPercent);

            //Stoppe Pflanzenwachsum wenn growthPercent 1 überschreitet, ansonsten wächst sie weiter (growing = true).
            growing = growthPercent < 1;
            for (int i = 0; i < flowers.Length; i++)
            {
                //Wenn Faktor für das Stengelwachstum der Pflanze größer ist, als der Start-Grow-Wert der Blüten, dürfen die Blätter skaliert werden/ wachsen
                if (growthPercent > flowerStartGrowTime[i])
                {
                    flowers[i].localScale = Vector3.MoveTowards(flowers[i].localScale, Vector3.one, Time.deltaTime / flowerGrowTime);

                    //Solange Blätter nicht auf Normalgröße sind, growing = true
                    if (flowers[i].localScale != Vector3.one)
                    {
                        growing = true;
                    }
                }
            }

            //Wenn sie mit dem Wachsen fertig ist und füge sie zum Garten hinzu.
            if (!growing)
            {
                FindObjectOfType<Garden>().AddPlant(this);
            }
        }
    }
}