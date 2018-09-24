using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyDraw : MonoBehaviour 
{
    public float lineWidth = 0.1f;
    public Color lineColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public float[,] points;

    LineRenderer lineRenderer;
    Vector3[] positions;


	// Use this for initialization
	void Awake () 
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    void Start()
    {
        positions = new Vector3[points.GetLength(0)];

        for (int i = 0; i < points.GetLength(0); i++)
        {
            positions[i] = new Vector3(points[i, 0], points[i, 1], 1.0f);
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update ()
    {
        lineRenderer.SetPositions(positions);
	}
}
