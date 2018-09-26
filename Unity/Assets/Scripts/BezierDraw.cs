using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using src;

public class BezierDraw : MonoBehaviour {
    public Bezier b;
    public int steps = 100;
    public Toggle tPoly, tPts, tCrv;
    public bool isSelected = false;

    private List<Bezier.BPoint> points;

    GameObject controlPoly, controlPts, curve;

    public void SetToggles(Toggle _tPoly, Toggle _tPts, Toggle _tCrv)
    {
        tPoly = _tPoly;
        tPts = _tPts;
        tCrv = _tCrv;

        tPoly.onValueChanged.AddListener(delegate {controlPoly.SetActive(!controlPoly.activeSelf);});
        tPts.onValueChanged.AddListener(delegate  {controlPts.SetActive(!controlPts.activeSelf);});
        tCrv.onValueChanged.AddListener(delegate  {curve.SetActive(!curve.activeSelf);});
    }

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
            Vector3 currentPoint = new Vector3(points[i].x, points[i].y, -1.0F);

            // Draw Circles on screen
            GameObject circle = new GameObject();
            circle.transform.SetParent(controlPts.transform);
            circle.AddComponent<CircleDraw>();
            circle.transform.position = currentPoint;

            CircleCollider2D circleCollider = circle.AddComponent<CircleCollider2D>();
            circleCollider.offset = new Vector2(0.0f, 0.0f);
            circleCollider.radius = 0.1f;
        }

        // Draw Curve
        Bezier.BPoint? previousPoint = new Bezier.BPoint (points[0].x, points[0].y);

        for (int i = 1; i <= steps; i++)
        {
            float currentT = (float)i/(float)steps;
            Bezier.BPoint? currentPoint = b.EvaluateAt(currentT); 

            //Construct one line
            GameObject lineSeg = new GameObject();
            lineSeg.transform.SetParent(curve.transform);
            PolyDraw currentLine = lineSeg.AddComponent<PolyDraw>();
            currentLine.points = new List<Bezier.BPoint> { previousPoint.GetValueOrDefault(), currentPoint.GetValueOrDefault() };

            previousPoint = currentPoint;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isSelected)
        {

        }
	}
}
