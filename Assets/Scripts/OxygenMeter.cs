using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class OxygenMeter : MonoBehaviour
{
    public Text percentageText;

    private double flowerPercentage = 0.05;
    private double treePercentage = 0.25;
    private double goalPercentage = 21;
    private double currentPercentage;

    //Update wird einmal pro Frame aufgerufen
    void Update()
    {
        if (currentPercentage < 21)
        {
            //suche alle Objekte mit dem Tag "Daffodil" und "Rose", z�hle sie
            List<GameObject> flowers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Daffodil"));
            flowers.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Rose")));
            int flowerCount = flowers.Count;

            //suche alle Objekte mit dem Tag "Tree", z�hle sie
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            int treeCount = trees.Length;

            //Berechnung der aktuellen Sauerstoffs�ttigung
            currentPercentage = flowerCount * flowerPercentage + treeCount * treePercentage;
            percentageText.text = currentPercentage.ToString() + "%";
        }
        else
        {
            percentageText.text = goalPercentage.ToString() + "%"; //Wenn bereits 21% erreicht sind, dann zeige immer 21% an.
        }
    }




    public double getCurrentPercentage()
    {
        return currentPercentage;
    }

}
