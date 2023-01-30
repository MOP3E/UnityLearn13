using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : OneColliderTrigger
{
    /// <summary>
    /// Скрипт перемещения мяча.
    /// </summary>
    [SerializeField] private BallMovement _ballMovement;

    /// <summary>
    /// Событие соприкосновения мяча с сегментом.
    /// </summary>
    [HideInInspector] public UnityEvent<SegmentType, Collider> CollisionWithSegment;

    /// <summary>
    /// Попадание мяча в сегмент.
    /// </summary>
    /// <param name="other"></param>
    protected override void OnOneTriggerEnter(Collider other)
    {
        if(_ballMovement == null) return;

        //получить сегмент
        Segment segment = other.GetComponent<Segment>();
        if (segment == null) return;

        switch (segment.SegmentType)
        {
            case SegmentType.Empty:
                //пустой сегмент - упасть на нижний этаж
                _ballMovement.Fall(other.transform.position.y);
                break;
            case SegmentType.Default:
                //обычный сегмент - продолжать прыгать
                _ballMovement.Jump();
                break;
            case SegmentType.Trap:
            case SegmentType.Finish:
                //ловушка или конец игры - остановиться
                _ballMovement.Stop();
                break;
        }

        //вызвать событие столкновения с сегментом
        if (CollisionWithSegment != null) CollisionWithSegment.Invoke(segment.SegmentType, other);
    }

    /// <summary>
    /// Выход мяча из сегмента.
    /// </summary>
    protected override void OnOneTriggerExit(Collider other)
    {
        //чтобы не мусорить в лог тестовыми сообещнями
    }
}
