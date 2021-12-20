using UnityEngine;
using UnityEngine.UI;

public class TreeCounter : MonoBehaviour
{
    public Text treeText;

    // Update is called once per frame
    void Update()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        int treeCount = trees.Length;
        treeText.text = treeCount.ToString();
    }
}
