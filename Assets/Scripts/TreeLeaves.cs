using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLeaves : MonoBehaviour
{
    public float scaleFactor;
    public OxygenMeter oxygen;
    public Transform[] flowers;
    private float step = 0f;

    //Finale Größe der Blätter
    Vector3 aimVector = Vector3.one * 20;


    void Start()
    {
        //Skalieren der Pflanze
        transform.localScale = Vector3.one * Random.Range(0.2f, .3f);

        for (int i = 0; i < flowers.Length; i++)
        {
            flowers[i].localScale = Vector3.zero * scaleFactor;
        }
    }

    // Update is called once per frame
    async void Update()
    {
        //Wachstum der Blätter abhängig von Sauerstoffgehalt

        if (oxygen.getCurrentPercentage() > 5)
        {
            step = 0.0005f;
        }
        if (oxygen.getCurrentPercentage() > 10)
        {
            step = 0.001f;
        }
        if (oxygen.getCurrentPercentage() > 15)
        {
            step = 0.0025f;
        }
        if (oxygen.getCurrentPercentage() > 20)
        {
            step = 0.004f;
        }

        for (int i = 0; i < flowers.Length; i++)
        {
            //MoveTowards(Startwert, Endwert, Schritte)
            flowers[i].localScale = Vector3.MoveTowards(flowers[i].localScale, aimVector, step);
        }
    }
}

