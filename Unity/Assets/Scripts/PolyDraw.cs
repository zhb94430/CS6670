using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using src;

public class PolyDraw : MonoBehaviour 
{
    public List<Bezier.BPoint> points;
    
    public float lineWidth = 0.025f;
    public Color lineColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    

    LineRenderer lineRenderer;
    Vector3[] positions;


	// Use this for initialization
	void Awake () 
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));

    }

    void Start()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        positions = new Vector3[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            positions[i] = new Vector3(points[i].x, points[i].y, 1.0f);
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update ()
    {
        // lineRenderer.SetPositions(positions);
	}
}
