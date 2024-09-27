using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class SplineEditor : MonoBehaviour
{
    #region Editor Variables
    [SerializeField] GameObject pointPrefab;
    [SerializeField] string filename = "Spline";
    [SerializeField] CurveType curveType = CurveType.Hermitian;
    [SerializeField] int curvePrecision = 20;

    Spline spline;
    Curve curve;
    public List<SplinePoint> splinePoints;

    LineRenderer lineRenderer;
    #endregion

    #region MonoBehavior Methods

    // Start is called before the first frame update
    void Start()
    {
        spline = new Spline();
        splinePoints = new List<SplinePoint>();

        spline.AddCurve();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
    }

    // Update is called once per frame
    void Update()
    {
        if (spline == null)
        {
            spline = new Spline();
            splinePoints = new List<SplinePoint>();

            SplinePoint point = Instantiate(pointPrefab, spline.points[0], Quaternion.identity).GetComponent<SplinePoint>();
            point.editor = this;
            point.index = 0;
            splinePoints.Add(point);
        }

        if (curve != null && curve.type != curveType)
        {
            curve.type = curveType;
        }

        Refresh();

        lineRenderer.startColor = Color.magenta;
        lineRenderer.endColor = Color.magenta;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        if (curvePrecision > 0)
        {
            lineRenderer.positionCount = curvePrecision * spline.curves.Count;
        }

        for (int i = 0; i < spline.curves.Count; i++)
        {
            for (int j = 0; j < curvePrecision; j++)
            {
                lineRenderer.SetPosition(i * curvePrecision + j, spline.curves[i].Compute((float)j / curvePrecision));
            }
        }
    }
    #endregion

    #region UI Methods

    public void AddCurve()
    {
        curve = spline.AddCurve();

        for (int i = 3; i > 0; i--)
        {
            SplinePoint point = Instantiate(pointPrefab, spline.points[spline.points.Count - i], Quaternion.identity).GetComponent<SplinePoint>();
            point.editor = this;
            point.index = splinePoints.Count;
            splinePoints.Add(point);
        }
    }

    public void Load()
    {
        spline.Load(filename);
    }

    public void Save()
    {
        spline.Save(filename);
    }

    public void Clear()
    {
        for (int i = 0; i < splinePoints.Count; i++)
        {
            SplinePoint point = splinePoints[i];
            DestroyImmediate(point.gameObject);
        }

        splinePoints.Clear();
        curve = null;
        spline.Clear();

        SplinePoint newPoint = Instantiate(pointPrefab, spline.points[0], Quaternion.identity).GetComponent<SplinePoint>();
        newPoint.editor = this;
        newPoint.index = 0;
        splinePoints.Add(newPoint);
    }

    public void Refresh()
    {
        if (splinePoints.Count != spline.points.Count)
        {
            for (int i = 0; i < splinePoints.Count; i++)
            {
                DestroyImmediate(splinePoints[i].gameObject);
            }

            splinePoints.Clear();

            for (int i = 0; i < spline.points.Count; i++)
            {
                SplinePoint newPoint = Instantiate(pointPrefab, spline.points[i], Quaternion.identity).GetComponent<SplinePoint>();
                newPoint.editor = this;
                newPoint.index = i;
                splinePoints.Add(newPoint);
            }
        }
        else
        {
            for (int i = 0; i < splinePoints.Count; i++)
                spline.points[i] = splinePoints[i].transform.position;
        }
    }

    #endregion
}
