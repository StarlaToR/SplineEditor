using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    [HideInInspector] public Spline spline;

    [SerializeField] public Transform startPoint;
    [SerializeField] string splineFileName = "Spline3";
    [SerializeField] float speed = 1f;

    bool isReturning = true;
    float currentSplinePos = 0;
    int currentCurve = 0;

    // Start is called before the first frame update
    void Start()
    {
        spline = new Spline();
        spline.Load(splineFileName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            spline.Load(splineFileName);
        }

        if (isReturning)
        {
            currentSplinePos -= Time.deltaTime;

            if (currentSplinePos < 0)
            {
                currentCurve -= 1;
                currentSplinePos = 1;
            }

            if (currentCurve < 0)
            {
                currentCurve = 0;
                currentSplinePos = 0;
                isReturning = false;
            }
        }
        else
        {
            currentSplinePos += Time.deltaTime;

            if (currentSplinePos > 1)
            {
                currentCurve += 1;
                currentSplinePos = 0;
            }

            if (currentCurve >= spline.curves.Count)
            {
                currentCurve = spline.curves.Count - 1;
                currentSplinePos = 1;
                isReturning = true;
            }
        }

        transform.SetPositionAndRotation(startPoint.position + spline.curves[currentCurve].Compute(currentSplinePos), Quaternion.identity);
    }
}
