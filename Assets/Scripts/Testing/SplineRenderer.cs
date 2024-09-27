using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineRenderer : MonoBehaviour
{
    [SerializeField] SplineFollower follower;
    [SerializeField] int curvePrecision = 20;

    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Spline spline = follower.spline;

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
                lineRenderer.SetPosition(i * curvePrecision + j, transform.position + spline.curves[i].Compute((float)j / curvePrecision));
            }
        }
    }
}
