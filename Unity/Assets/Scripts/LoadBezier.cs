using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using src;

public class LoadBezier : MonoBehaviour {
    public string path;

    public DatParser dparser;

	// Use this for initialization
	void Awake () {
        // TODO Use DatParser class to load files

        // Hard Coded for now
        dparser = new DatParser(path);
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Bezier b in dparser.GetResult().GetBezierList())
        {
            GameObject g = new GameObject();

            BezierDraw bScript = g.AddComponent<BezierDraw>();

            bScript.b = b;
        }

	}
}
