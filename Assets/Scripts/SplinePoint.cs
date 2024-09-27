using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinePoint : MonoBehaviour
{
    public int index;
    [HideInInspector] public SplineEditor editor;

    // Update is called once per frame
    void Update()
    {
        if (editor.splinePoints[index].gameObject != gameObject)
            DestroyImmediate(gameObject);
    }
}
