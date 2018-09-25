using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using src;

public class BezierDraw : MonoBehaviour {
    public Bezier b;
    public int steps = 100;

    float[,] points;

    GameObject controlPoly, controlPts, curve;
    bool controlPolyToggle, controlPtsToggle, curveToggle = true;

	// Use this for initialization
	void Start () {
        points = b.GetPoints();
        controlPoly = new GameObject();
        controlPts = new GameObject();
        curve = new GameObject();


        // Draw Polygon
        PolyDraw p = controlPoly.AddComponent<PolyDraw>();
        p.points = points;
        p.lineColor = new Color(0.0f, 0.0f, 1.0f);

        // Draw Points
        for (int i = 0; i < b.numOfPoints; i++)
        {
            // Hardcoded to two dim for now
            Vector3 currentPoint = new Vector3(points[i, 0], points[i, 1], -1.0F);

            // Draw Circles on screen
            GameObject circle = new GameObject();
            circle.transform.SetParent(controlPts.transform);
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
            GameObject lineSeg = new GameObject();
            lineSeg.transform.SetParent(curve.transform);
            PolyDraw currentLine = lineSeg.AddComponent<PolyDraw>();
            currentLine.points = new float[,] { { previousPoint[0], previousPoint[1] },
                                                { currentPoint[0] , currentPoint[1] } };

            previousPoint = currentPoint;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
