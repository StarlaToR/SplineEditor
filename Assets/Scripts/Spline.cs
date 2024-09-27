using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline
{
    public List<Vector3> points = new List<Vector3>();
    public List<Curve> curves = new List<Curve>();

    public Spline()
    {
        points = new List<Vector3>();
        points.Add(Vector3.zero);

        curves = new List<Curve>();
    }

    ~Spline()
    {
        points.Clear();
        curves.Clear();
    }

    public void Clear()
    {
        points.Clear();
        curves.Clear();

        points.Add(Vector3.zero);
    }

    public Curve AddCurve(int type = 0)
    {
        Curve newCurve = new Curve(this, curves.Count * 3, type);
        curves.Add(newCurve);

        Vector3 lastPoint = points[points.Count - 1];

        points.Add(lastPoint + Vector3.right);
        points.Add(lastPoint + Vector3.right * 2);
        points.Add(lastPoint + Vector3.right * 3);

        return newCurve;
    }

    public List<Vector3> GetCurvePoints(int firstIndex)
    {
        List<Vector3> curvePoints = new List<Vector3>();

        for (int i = 0; i < 4; i++)
            curvePoints.Add(points[firstIndex + i]);

        return curvePoints;
    }

    #region Save & Load Methods
    public void Save(string name)
    {
        string path = Application.dataPath + "/" + name + ".txt";
        string data = "";

        for (int i = 0; i < points.Count; i++)
        {
            if (i != 0) data += ";";
            data += points[i].x.ToString() + " " + points[i].y.ToString() + " " + points[i].z.ToString();
        }

        data += "\n";

        for (int i = 0; i < curves.Count; i++)
        {
            if (i != 0) data += ";";
            data += ((int)curves[i].type).ToString();
        }

        File.WriteAllText(path, data);
    }

    public void Load(string name)
    {
        curves.Clear();
        points.Clear();

        string path = Application.dataPath + "/" + name + ".txt";
        string data = File.ReadAllText(path);

        string[] splittedData = data.Split('\n');

        string[] pointsStr = splittedData[0].Split(';');

        for (int i = 0; i < pointsStr.Length; i++)
        {
            string[] vectorStr = pointsStr[i].Split(' ');

            Vector3 point = new Vector3(float.Parse(vectorStr[0]), float.Parse(vectorStr[1]), float.Parse(vectorStr[2]));

            points.Add(point);
        }

        string[] curvesStr = splittedData[1].Split(';');

        for (int i = 0; i < curvesStr.Length; i++)
        {
            int type = int.Parse(curvesStr[i]);

            List<Vector3> curvePoints = new List<Vector3>();
            for (int j = 0; j < 4; j++)
                curvePoints.Add(points[i * 3 + j]);

            curves.Add(new Curve(this, curves.Count * 3, type));
        }
    }
    #endregion
}
