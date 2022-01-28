using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTimer : MonoBehaviour
{

    public TextMeshProUGUI text;

    float startValue;
    public float hinweisAnzeigeInSekunden;


    // Start is called before the first frame update
    void Start()
    {
        startValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        startValue += Time.deltaTime;
        if (startValue >= hinweisAnzeigeInSekunden)
            text.gameObject.active = false;


    }
}
