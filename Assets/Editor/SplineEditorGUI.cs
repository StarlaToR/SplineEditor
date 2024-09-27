using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SplineEditor))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SplineEditor splineEditor = (SplineEditor)target;

        //if (DrawDefaultInspector())
        //{
        //    splineEditor.DrawSpline();
        //}

        //if (GUILayout.Button("Draw Spline"))
        //{
        //    splineEditor.DrawSpline();
        //}

        if (GUILayout.Button("Add Curve"))
        {
            splineEditor.AddCurve();
        }

        if (GUILayout.Button("Refresh Spline"))
        {
            splineEditor.Refresh();
        }

        if (GUILayout.Button("Save Spline"))
        {
            splineEditor.Save();
        }

        if (GUILayout.Button("Load Spline"))
        {
            splineEditor.Load();
        }

        if (GUILayout.Button("Clear Spline"))
        {
            splineEditor.Clear();
        }
    }
}
