using System;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        var startPos = _start.position;
        var endPos = _end.position;
        _lineRenderer.SetPositions(new []{startPos, endPos});
    }
}
