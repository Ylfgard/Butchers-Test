using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineDrawer : MonoBehaviour
{
    private SplineConverter _splineConverter;
    private SplineComputer _splineComputer;
    private List<SplinePoint> _splinePoints;

    private void Awake()
    {
        _splineConverter = FindObjectOfType<SplineConverter>();
        _splineComputer = gameObject.GetComponent<SplineComputer>();
        _splinePoints = new List<SplinePoint>();
        ClearSpline();
    }

    public void DrawingBegan()
    {
        ClearSpline();
    }

    public void DrawingContinues(Vector3 inputPos)
    {
        _splinePoints.Add(new SplinePoint(inputPos));
        _splineComputer.SetPoints(_splinePoints.ToArray());
    }

    public void DrawingEnded()
    {
        List<Vector3> points = new List<Vector3>();
        foreach(SplinePoint splinePoint in _splinePoints)
            points.Add(splinePoint.position); 
        Vector3 side1 = points[0] - points[1];
        Vector3 side2 = points[2] - points[1];
        Vector3 normal = Vector3.Cross(side1, side2).normalized;
        _splineConverter.ConvertSpline(points, normal);
        ClearSpline();
    }

    private void ClearSpline()
    {
        _splinePoints.Clear();
        _splineComputer.SetPoints(_splinePoints.ToArray());
    }
}
