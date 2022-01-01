using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class FlowerCounter : MonoBehaviour
{
    public Text flowerText;

    //Update wird einmal pro Frame aufgerufen
    void Update() {
        //suche alle Objekte mit dem Tag "Daffodil" und "Rose", zähle sie und ersetze den Text des Counters
        List<GameObject> flowers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Daffodil"));
        flowers.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Rose")));

        int flowerCount = flowers.Count;
        flowerText.text = flowerCount.ToString();
    }
}
