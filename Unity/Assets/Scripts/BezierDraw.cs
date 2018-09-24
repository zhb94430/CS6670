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
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < b.numOfPoints; i++)
        {
            // Hardcoded to two dim for now
            Vector3 currentPoint = new Vector3(points[i, 0], points[i, 1], 0.0F);

            // Draw on screen
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = currentPoint;
        }
	}
}
