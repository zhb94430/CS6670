using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using src;
using SFB;

public class BezierController : MonoBehaviour {
    public string path;
    public Canvas canvas;
    public Toggle tPoly, tPts, tCrv;

    public DatParser dparser;
    //public CrvParser cparser;

    Vector3 prevMousePos;
    Vector3 startPos;
    bool moving = false;
    GameObject movingObject = null;

	// Use this for initialization
	void Awake () {
        // TODO Use DatParser class to load files

        // Hard Coded for now
        dparser = new DatParser(path);
	}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D h = Physics2D.GetRayIntersection(r, Mathf.Infinity);

            if (h.collider != null)
            {
                movingObject = h.collider.gameObject;
                movingObject.GetComponent<CircleDraw>().ChangeColor(new Color(1.0f, 0.0f, 0.0f));
                startPos = movingObject.transform.position;

                moving = true;
            }
        }

        if (moving)
        {
            Vector3 screenTranslation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(prevMousePos);
            Vector3 worldTranslation = new Vector3(screenTranslation.x, screenTranslation.y, 0);

            movingObject.transform.Translate(worldTranslation);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D h = Physics2D.GetRayIntersection(r, Mathf.Infinity);

            if (h.collider != null)
            {
                h.collider.gameObject.GetComponent<CircleDraw>().ChangeColorToDefault();
                movingObject = null;
                moving = false;


            }
        }

        prevMousePos = Input.mousePosition;
    }

    public void LoadCurveList ()
    {
        foreach (Bezier b in dparser.GetResult().GetBezierList())
        {
            GameObject g = new GameObject();

            BezierDraw bScript = g.AddComponent<BezierDraw>();
            bScript.SetToggles(tPoly, tPts, tCrv);

            bScript.b = b;
            bScript.steps = 100;
        }
    }

    public void DisplayCrv()
    {

    }

    public void OpenNewFile ()
    {
        string[] result = StandaloneFileBrowser.OpenFilePanel("Choose .dat file", "../", "dat", false);

        if (result.Length == 1)
        {
            dparser = new DatParser(result[0].Substring(7));

            LoadCurveList();
        }
    }

    public void OpenCRVFile()
    {

    }
}
