using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurveType
{
    Hermitian,
    Bezier,
    B,
    CatmullRom,
}

public class Curve
{
    public int firstPointIndex;
    public CurveType type = CurveType.Hermitian;

    Spline spline;

    #region Constructors
    public Curve(Spline spl, int index, int curveType)
    {
        spline = spl;
        type = (CurveType)curveType;

        firstPointIndex = index;
    }
    #endregion

    #region Curve Methods
    public Vector3 Compute(float t)
    {
        switch (type)
        {
            case CurveType.Hermitian:
                return ComputeHermitian(t);
            case CurveType.Bezier:
                return ComputeBezier(t);
            case CurveType.B:
                return ComputeB(t);
            case CurveType.CatmullRom:
                return ComputeCatmullRom(t);
            default:
                Debug.LogError("Curve type incorrect. Computing cancelled.");
                return Vector3.zero;
        }
    }

    Vector3 ComputeHermitian(float t)
    {
        List<Vector3> points = spline.GetCurvePoints(firstPointIndex);

        float tp3 = t * t * t;
        float tp2 = t * t;

        Vector3 P1 = points[0];
        Vector3 P2 = points[3];

        Vector3 R1 = points[1] - P1;
        Vector3 R2 = P2 - points[2];

        return (2f * tp3 - 3f * tp2 + 1f) * P1 + (-2f * tp3 + 3f * tp2) * P2 + (tp3 - 2f * tp2 + t) * R1 + (tp3 - tp2) * R2;
    }

    Vector3 ComputeBezier(float t)
    {
        List<Vector3> points = spline.GetCurvePoints(firstPointIndex);

        float tp3 = t * t * t;
        float tp2 = t * t;
        float it = 1 - t;
        float itp3 = it * it * it;
        float itp2 = it * it;

        Vector3 P1 = points[0];
        Vector3 P2 = points[1];
        Vector3 P3 = points[2];
        Vector3 P4 = points[3];

        return itp3 * P1 + (3f * t * itp2) * P2 + (3f * tp2 * it) * P3 + tp3 * P4;
    }

    Vector3 ComputeB(float t)
    {
        List<Vector3> points = spline.GetCurvePoints(firstPointIndex);

        float tp3 = t * t * t;
        float tp2 = t * t;
        float itp3 = (1 - t) * (1 - t) * (1 - t);

        Vector3 P1 = points[0];
        Vector3 P2 = points[1];
        Vector3 P3 = points[2];
        Vector3 P4 = points[3];

        return 1f / 6f * (itp3 * P1 + (3f * tp3 - 6f * tp2 + 4) * P2 + (-3f * tp3 + 3f * tp2 + 3f * t + 1f) * P3 + tp3 * P4);
    }

    Vector3 ComputeCatmullRom(float t)
    {
        List<Vector3> points = spline.GetCurvePoints(firstPointIndex);

        float tp3 = t * t * t;
        float tp2 = t * t;

        Vector3 P1 = points[0];
        Vector3 P2 = points[1];
        Vector3 P3 = points[2];
        Vector3 P4 = points[3];

        return 1f / 2f * ((-tp3 + 2 * tp2 - t) * P1 + (3f * tp3 - 5f * tp2 + 1) * P2 + (-3f * tp3 + 4f * tp2 + t) * P3 + (tp3 - tp2) * P4);
    }
    #endregion
}
