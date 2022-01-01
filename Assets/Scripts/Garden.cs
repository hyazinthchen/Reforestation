using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//jedes Unity-Script erbt von der Klasse MonoBehaviour
public class Garden : MonoBehaviour {
    public float boundsSize = 10;
    public int numDivisions = 10;
    List<Plant>[,] cells;

    public int testX;
    public int testY;

    public Transform test;

    public bool gizmosOnlyWhenSelected;

    //Wird vor dem allerersten Frame-Update aufgerufen.
    void Start() {
        cells = new List<Plant>[numDivisions, numDivisions];
        for (int i = 0; i < numDivisions; i++) {
            for (int j = 0; j < numDivisions; j++) {
                cells[i, j] = new List<Plant>();
            }
        }
    }

    public void AddPlant(Plant plant) {
        float posX = plant.transform.position.x;
        float posY = plant.transform.position.z;

        float cellSize = boundsSize / numDivisions;
        int cellX = Mathf.Clamp((int)((posX + boundsSize / 2) / cellSize), 0, numDivisions - 1);
        int cellY = Mathf.Clamp((int)((posY + boundsSize / 2) / cellSize), 0, numDivisions - 1);

        cells[cellX, cellY].Add(plant);
    }

    void DrawGizmos() {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube (Vector3.up * transform.position.y, new Vector3 (boundsSize, 0, boundsSize));
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
            //Gizmos.DrawRay (topLeft + Vector3.forward * x / (float) numDivisions * boundsSize, Vector3.right * boundsSize);
            // Gizmos.DrawRay (topLeft + Vector3.right * x / (float) numDivisions * boundsSize, Vector3.forward * boundsSize);
        }
    }

    void DrawProjectedLineGizmo(Vector3 a, Vector3 b) {
        float h1 = Terrain.activeTerrain.SampleHeight(a);
        float h2 = Terrain.activeTerrain.SampleHeight(b);

        Gizmos.DrawLine(new Vector3(a.x, h1, a.z), new Vector3(b.x, h2, b.z));
    }

    void OnDrawGizmosSelected() {
        if (gizmosOnlyWhenSelected) {
            DrawGizmos();
        }
    }

    void OnDrawGizmos() {
        if (!gizmosOnlyWhenSelected) {
            DrawGizmos();
        }
    }
}
