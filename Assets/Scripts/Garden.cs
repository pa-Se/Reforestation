using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
//Klasse zum Erfassen der gepflanzten Pflanzen -separat vom Terrain
public class Garden : MonoBehaviour
{

    //Gitterdefinition
    public float boundsSize = 10;
    public int numDivisions = 10;

    //Pflanzenliste
    List<Plant>[,] cells;

    //Gizmo(Gitter) nur anzeigen wenn, auf true
    public bool gizmosOnlyWhenSelected;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start()
    {   //Für jedes Feld des Gitters wird eine eigene Pflanzenliste erstellt
        cells = new List<Plant>[numDivisions, numDivisions];
        for (int i = 0; i < numDivisions; i++)
        {
            for (int j = 0; j < numDivisions; j++)
            {
                cells[i, j] = new List<Plant>();
            }
        }
    }

    public void AddPlant(Plant plant)
    {

        //Position bestimmen von Pflanze/Samen
        float posX = plant.transform.position.x;
        float posY = plant.transform.position.z;

        //
        float cellSize = boundsSize / numDivisions;

        //Erster Parameter ist der Wert, der nur zurückgegeben wird, wenn er innerhalb 0 und numDivisions liegt
        //Genaue Zelle wird berechnet
        int cellX = Mathf.Clamp((int)((posX + boundsSize / 2) / cellSize), 0, numDivisions - 1);
        int cellY = Mathf.Clamp((int)((posY + boundsSize / 2) / cellSize), 0, numDivisions - 1);

        //Hinzufügen der Pflanze zur örtlich, richtigen Zelle
        cells[cellX, cellY].Add(plant);
    }

    void DrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Initialisieren von einem Vektor um von dort Gitter zu zeichnen
        Vector3 topLeft = Vector3.left * boundsSize / 2 + Vector3.forward * -boundsSize / 2 + Vector3.up * transform.position.y;
        int numSteps = 30;
        for (int x = 1; x < numDivisions; x++)
        {
            for (int step = 0; step < numSteps; step++)
            {
                float p1 = step / (float)numSteps;
                float p2 = (step + 1) / (float)numSteps;
                Vector3 startX = topLeft + Vector3.forward * x / (float)numDivisions * boundsSize + Vector3.right * boundsSize * p1;
                Vector3 endX = topLeft + Vector3.forward * x / (float)numDivisions * boundsSize + Vector3.right * boundsSize * p2;
                DrawProjectedLineGizmo(startX, endX);
                Vector3 startY = topLeft + Vector3.right * x / (float)numDivisions * boundsSize + Vector3.forward * boundsSize * p1;
                Vector3 endY = topLeft + Vector3.right * x / (float)numDivisions * boundsSize + Vector3.forward * boundsSize * p2;
                DrawProjectedLineGizmo(startY, endY);
            }

        }
    }

    void DrawProjectedLineGizmo(Vector3 a, Vector3 b)
    {

        //TerrainHöhe an Vektor a, b ermitteln 
        float h1 = Terrain.activeTerrain.SampleHeight(a);
        float h2 = Terrain.activeTerrain.SampleHeight(b);

        //Zeichnen des Gitters
        Gizmos.DrawLine(new Vector3(a.x, h1, a.z), new Vector3(b.x, h2, b.z));
    }
}
