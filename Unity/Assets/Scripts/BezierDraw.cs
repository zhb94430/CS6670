using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using src;

public class BezierDraw : MonoBehaviour {
    public Bezier b;
    public int steps = 100;

    float[,] points;

	// Use this for initialization
	void Start () {
        points = b.GetPoints();

        // Draw Polygon
        GameObject polyLine = new GameObject();
        PolyDraw p = polyLine.AddComponent<PolyDraw>();
        p.points = points;

        // Draw Points
        for (int i = 0; i < b.numOfPoints; i++)
        {
            // Hardcoded to two dim for now
            Vector3 currentPoint = new Vector3(points[i, 0], points[i, 1], -1.0F);

            // Draw Circles on screen
            GameObject circle = new GameObject();
            circle.AddComponent<CircleDraw>();
            circle.transform.position = currentPoint;
        }

        // Draw Curve
        float[] previousPoint = new float[2] {points[0,0], points[0,1]};

        for (int i = 1; i <= steps; i++)
        {
            float currentT = (float)i/(float)steps;
            float[] currentPoint = b.EvaluateAt(currentT); 

            //Construct one line
            GameObject gameObject = new GameObject();
            PolyDraw currentLine = gameObject.AddComponent<PolyDraw>();
            currentLine.points = new float[,] {previousPoint, currentPoint};

            previousPoint = currentPoint;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
