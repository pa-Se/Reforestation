using UnityEngine;
using UnityEngine.UI;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class TreeCounter : MonoBehaviour
{
    public Text treeText;

    //Update wird einmal pro Frame aufgerufen
    void Update()
    {
        //suche alle Objekte mit dem Tag "Tree", z�hle sie und ersetze den Text des Counters
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        int treeCount = trees.Length;
        treeText.text = treeCount.ToString();
    }
}
