using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    /// <summary>
    /// Тип сегмента.
    /// </summary>
    [SerializeField] private SegmentType _segmentType;

    /// <summary>
    /// Тип сегмента.
    /// </summary>
    public SegmentType SegmentType => _segmentType;

    /// <summary>
    /// Растеризатор сегмента.
    /// </summary>
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// Настройка сегмента согласно типа.
    /// </summary>
    /// <param name="type">Требуемый тип сегмента.</param>
    public void SetType(SegmentType type)
    {
        _segmentType = type;
        switch (_segmentType)
        {
            case SegmentType.Default:
                _meshRenderer.enabled = true;
                break;
            case SegmentType.Trap:
                _meshRenderer.enabled = true;
                break;
            case SegmentType.Empty:
                _meshRenderer.enabled = false;
                break;
            case SegmentType.Finish:
                _meshRenderer.enabled = true;
                break;
        }
    }

    /// <summary>
    /// Задать материал сегмента.
    /// </summary>
    public void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
