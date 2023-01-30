using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallEvents : MonoBehaviour
{
    /// <summary>
    /// Контроллер мяча.
    /// </summary>
    [SerializeField] internal BallController _ballController;

    /// <summary>
    /// Подписаться на событие мяча.
    /// </summary>
    protected virtual void Awake()
    {
        _ballController.CollisionWithSegment.AddListener(OnCollisionWithSegment);
    }

    /// <summary>
    /// Отписаться от события мяча.
    /// </summary>
    protected virtual void OnDestroy()
    {
        _ballController.CollisionWithSegment.RemoveListener(OnCollisionWithSegment);
    }

    /// <summary>
    /// Обработчик события столкновения мяча с сегментом.
    /// </summary>
    /// <param name="segmentType"></param>
    protected virtual void OnCollisionWithSegment(SegmentType segmentType, Collider other) { }
}
