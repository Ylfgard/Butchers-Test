using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineConverter : MonoBehaviour
{
    [SerializeField]
    private Transform _planeTransf;
    [SerializeField]
    private float _scale;
    private SplineComputer _splineComputer;
    private Vector3 _normal;

    private void Awake()
    {
        _splineComputer = gameObject.GetComponent<SplineComputer>();
        _normal = Vector3.up;
    }

    public void ConvertSpline(List<Vector3> points, Vector3 normal)
    {
        Matrix4x4 translateMatrix = Matrix4x4.Translate(normal);
        Quaternion quaternion = Quaternion.FromToRotation(normal, _normal);
        translateMatrix = Matrix4x4.Rotate(Quaternion.Euler(quaternion.eulerAngles));
        List<SplinePoint> splinePoints = new List<SplinePoint>(); 
        Vector3 center = Vector3.zero;
        foreach(Vector3 point in points)
            center += point;
        center /= points.Count;
        Vector3 offset = _planeTransf.position - translateMatrix.MultiplyVector(center);
         
        foreach(Vector3 point in points)
        {
            SplinePoint sp = new SplinePoint(); 
            sp.position = translateMatrix.MultiplyVector(point);
            sp.position += offset;
            sp.position += ((sp.position - _planeTransf.position) * _scale);
            splinePoints.Add(sp);
        }
        _splineComputer.SetPoints(splinePoints.ToArray());
        CalculatePoints();
    }

    private void CalculatePoints()
    {
        float length = _splineComputer.CalculateLength();
        
    }
}
