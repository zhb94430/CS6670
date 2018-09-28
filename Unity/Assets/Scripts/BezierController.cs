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

    private Curves curves;

    // Bezier Management Stuff
    List<Bezier> bezierList;
    GameObject[] bezierObjectList;
    BezierDraw[] bezierDrawList;

    // Index the bezier curves
    private int _activeIndex = 0;
    public int activeIndex
    {
        get
        {
            return _activeIndex;
        }
        set
        {
            if (value >= bezierDrawList.Length)
            {
                _activeIndex = bezierDrawList.Length - 1;
            }
            else if (value < 0)
            {
                _activeIndex = 0;
            }
            else 
            {
                _activeIndex = value;
            }
        }
    }

    // Interaction Varaibles
    Vector3 prevMousePos;
    Vector3 startPos;
    bool moving = false;
    GameObject movingObject = null;

	// Use this for initialization
	void Awake () {
        // Hard Coded for now
        dparser = new DatParser(path);
	}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {

        //Add Point
        if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftAlt))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D h = Physics2D.GetRayIntersection(r, Mathf.Infinity);

            if (h.collider != null)
            {
                Vector3 hitPosition = h.collider.gameObject.transform.position;
                Bezier.BPoint b = new Bezier.BPoint(hitPosition.x, hitPosition.y);

                AddPoint(b);
            }
        }

        //Delete Point
        if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftAlt))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D h = Physics2D.GetRayIntersection(r, Mathf.Infinity);

            if (h.collider != null)
            {
                Vector3 hitPosition = h.collider.gameObject.transform.position;
                Bezier.BPoint b = new Bezier.BPoint(hitPosition.x, hitPosition.y);

                DeletePoint(b);
            }
        }

            //Drag Point
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

                Vector3 endPos = movingObject.transform.position;
                Bezier.BPoint startPoint = new Bezier.BPoint(startPos.x, startPos.y);
                Bezier.BPoint endPoint = new Bezier.BPoint(endPos.x, endPos.y);

                bezierList[activeIndex].ReplacePointWith(startPoint, endPoint);

                ReloadCurveList();

                movingObject = null;
                moving = false;
            }
        }

        prevMousePos = Input.mousePosition;
    }

    public void ReloadCurveList()
    {
        Bezier b = bezierList[activeIndex];

        Destroy(bezierObjectList[activeIndex]);

        GameObject g = new GameObject();
        g.name = "BezierCurve" + activeIndex;

        BezierDraw bScript = g.AddComponent<BezierDraw>();
        bScript.SetToggles(tPoly, tPts, tCrv);

        bScript.b = b;
        bScript.steps = 100;
        bScript.isSelected = true;

        bezierObjectList[activeIndex] = g;
        bezierDrawList[activeIndex] = bScript;
    }

    public void LoadCurveList ()
    {
        bezierList = curves.GetBezierList();
        bezierObjectList = new GameObject[bezierList.Count];
        bezierDrawList = new BezierDraw[bezierList.Count];


        for (int i = 0; i < bezierList.Count; i++)
        {
            Bezier b = bezierList[i];

            GameObject g = new GameObject();
            g.name = "BezierCurve" + i;

            BezierDraw bScript = g.AddComponent<BezierDraw>();
            bScript.SetToggles(tPoly, tPts, tCrv);

            bScript.b = b;
            bScript.steps = 100;

            bezierObjectList[i] = g;
            bezierDrawList[i] = bScript;
        }
    }

    public void DisplayCrv()
    {

    }

    // Button Functions
    public void AddPoint(Bezier.BPoint bPoint)
    {
        Bezier b = bezierList[activeIndex];
        b.InsertPoint(bPoint);

        ReloadCurveList();
    }

    public void DeletePoint(Bezier.BPoint bPoint)
    {
        Bezier b = bezierList[activeIndex];
        b.DeletePoint(bPoint);

        ReloadCurveList();
    }

    public void NextCurve()
    {
        bezierDrawList[activeIndex].isSelected = false;
        activeIndex = activeIndex + 1;
        bezierDrawList[activeIndex].isSelected = true;
    }

    public void PrevCurve()
    {
        bezierDrawList[activeIndex].isSelected = false;
        activeIndex = activeIndex - 1;
        bezierDrawList[activeIndex].isSelected = true;
    }

    public void NewCurve()
    {
        List<Bezier.BPoint> PArray = new List<Bezier.BPoint> { new Bezier.BPoint(-5.0F, 0.0F),
                                                               new Bezier.BPoint(0.0F, 5.0F),
                                                               new Bezier.BPoint(5.0F, 0.0F) };
        Bezier newB = new Bezier(PArray);

        if (curves == null)
        {
            curves = new Curves();
        }
        else
        {
            foreach (GameObject g in bezierObjectList)
            {
                Destroy(g);
            }
        }

        curves.AddBezier(newB);

        //GameObject g = new GameObject();
        //g.name = "BezierCurve" + i;

        //BezierDraw bScript = g.AddComponent<BezierDraw>();
        //bScript.SetToggles(tPoly, tPts, tCrv);

        //bScript.b = newB;
        //bScript.steps = 100;
        //bScript.isSelected = true;

        //bezierObjectList[i] = g;
        //bezierDrawList[i] = bScript;

        LoadCurveList();
    }

    public void OpenNewFile ()
    {
        string[] result = StandaloneFileBrowser.OpenFilePanel("Choose .dat file", "../", "dat", false);

        if (result.Length == 1)
        {
            dparser = new DatParser(result[0].Substring(7));

            if (curves == null)
            {
                curves = dparser.Parse();
            }
            else
            {
                foreach (GameObject g in bezierObjectList)
                {
                    Destroy(g);
                }

                curves.Append(dparser.Parse());
            }


            LoadCurveList();
        }
    }

    public void SaveFile()
    {
        curves.SetBezierList(bezierList);

        //string stringToWrite = dparser.Unparse(curves);

        string result = StandaloneFileBrowser.SaveFilePanel("Choose Save Location", "../", "Curve", "dat");
    }

    public void OpenCRVFile()
    {

    }
}
