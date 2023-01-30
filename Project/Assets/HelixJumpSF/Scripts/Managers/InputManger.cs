using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManger : BallEvents
{
    /// <summary>
    /// Ручной вращатель уровня.
    /// </summary>
    [SerializeField] private ManualRotator _manualRotator;

    protected override void OnCollisionWithSegment(SegmentType segmentType, Collider other)
    {
        if (segmentType == SegmentType.Trap || segmentType == SegmentType.Finish)
        {
            _manualRotator.enabled = false;
        }
    }
}
