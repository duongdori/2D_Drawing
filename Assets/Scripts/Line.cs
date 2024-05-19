using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float distanceBetweenPoints = 0.1f;
    
    private List<Vector2> _points = new();
    
    public void SetupLine(float size, Color color)
    {
        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
    public void UpdateLine(Vector2 position)
    {
        if (_points.Count == 0)
        {
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(_points.Last(), position) > distanceBetweenPoints)
        {
            SetPoint(position);
        }
    }
    
    private void SetPoint(Vector2 point)
    {
        // min width: 0.02
        // max width: 0.3
        _points.Add(point);
        lineRenderer.positionCount = _points.Count;
        lineRenderer.SetPosition(_points.Count - 1, point);
    }
}
