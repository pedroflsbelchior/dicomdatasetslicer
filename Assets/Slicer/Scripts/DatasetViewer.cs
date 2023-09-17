using GD.MinMaxSlider;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DatasetViewer : MonoBehaviour
{
    [Header("Threshold Window")]
    [MinMaxSlider(0, 1)]
    public Vector2 window = Vector2.up;

    [Header("Axis Controls")]
    public bool flipXAxis = false;
    public bool flipYAxis = false;
    public bool flipZAxis = false;

    [Header("Dataset Limits")]
    public Transform DatasetMinAnchor = null;
    public Transform DatasetMaxAnchor = null;


    private Vector2 _window = Vector2.up;
    private Vector3 _flipAxis = Vector3.zero;
    private Vector3 _minPos = Vector3.zero;
    private Vector3 _maxPos = Vector3.zero;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _meshRenderer.material.SetVector("_window", _window);
        _meshRenderer.material.SetVector("_flipAxis", _flipAxis);
        _meshRenderer.material.SetVector("_min", _minPos);
        _meshRenderer.material.SetVector("_max", _maxPos);
    }

    void Update()
    {
        if (_window != window)
        {
            _window = window;
            _meshRenderer.material.SetVector("_window", _window);
        }

        bool dirtyAxis = false;
        if (flipXAxis && _flipAxis.x != 1) { _flipAxis.x = 1; dirtyAxis = true; }
        if (!flipXAxis && _flipAxis.x != 0) { _flipAxis.x = 0; dirtyAxis = true; }
        if (flipYAxis && _flipAxis.y != 1) { _flipAxis.y = 1; dirtyAxis = true; }
        if (!flipYAxis && _flipAxis.y != 0) { _flipAxis.y = 0; dirtyAxis = true; }
        if (flipZAxis && _flipAxis.z != 1) { _flipAxis.z = 1; dirtyAxis = true; }
        if (!flipZAxis && _flipAxis.z != 0) { _flipAxis.z = 0; dirtyAxis = true; }

        if (dirtyAxis)
        {
            _meshRenderer.material.SetVector("_flipAxis", _flipAxis);
        }

        if (_minPos != DatasetMinAnchor.localPosition)
        {
            _minPos = DatasetMinAnchor.localPosition;
            _meshRenderer.material.SetVector("_min", _minPos);
        }

        if (_maxPos != DatasetMaxAnchor.localPosition)
        {
            _maxPos = DatasetMaxAnchor.localPosition;
            _meshRenderer.material.SetVector("_max", _maxPos);
        }

        _meshRenderer.material.SetMatrix("_worldToLocal", DatasetMinAnchor.parent.worldToLocalMatrix);

    }
}
