using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using src;

public class BezierDraw : MonoBehaviour {
    public Bezier b;
    float[,] points;

	// Use this for initialization
	void Start () {
        points = b.GetPoints();

        // Init PolyDraw
        GameObject polyLine = new GameObject();
        PolyDraw p = polyLine.AddComponent<PolyDraw>();

        p.points = points;


        // Drawing Points
        for (int i = 0; i < b.numOfPoints; i++)
        {
            // Hardcoded to two dim for now
            Vector3 currentPoint = new Vector3(points[i, 0], points[i, 1], -1.0F);

            // Draw Circles on screen
            GameObject circle = new GameObject();
            circle.AddComponent<CircleDraw>();
            circle.transform.position = currentPoint;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
