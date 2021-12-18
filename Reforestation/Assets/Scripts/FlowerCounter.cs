using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerCounter : MonoBehaviour
{
    public Text flowerText;

    // Update is called once per frame
    void Update()
    {
        List<GameObject> flowers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Daffodil"));
        flowers.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Rose")));

        int flowerCount = flowers.Count;
        flowerText.text = flowerCount.ToString();
    }
}
