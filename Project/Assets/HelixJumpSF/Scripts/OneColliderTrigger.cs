using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneColliderTrigger : MonoBehaviour
{
    /// <summary>
    /// Трансформа уровня.
    /// </summary>
    [SerializeField] private Transform _levelTransform;

    /// <summary>
    /// Коллайдер, с которым столкнулся мячик.
    /// </summary>
    private Collider _other;

    /// <summary>
    /// Попадание в триггер.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //если мы уже попали сегмент, либо это не сегмент, ничего не делать
        if (_other != null || other == null) return;
        Segment segment = other.GetComponent<Segment>();
        if (segment == null) return;

        //вычислить поворот по оси Y сегмента
        float otherAngle = segment.transform.localEulerAngles.y;
        if (otherAngle < 0) otherAngle += 360;
        
        //вычислить поворот по оси Y уровня
        //почему-то уровень вращается в отрицательном направлении, а сегменты - в положительном
        float levelAngle = _levelTransform.eulerAngles.y;
        if(levelAngle > 0) levelAngle = 360 - levelAngle;
        //корректировка угла уровня для сегментов, переходящих через 0 градусов
        if (levelAngle < 30 && otherAngle >= 330) levelAngle += 360;
        //Debug.Log($"сегмент = {otherAngle};  уровень = {levelAngle}");

        //мячик не попадает в сегмент?
        //сегмент имеет угол в 30 градусов
        if (levelAngle < otherAngle || levelAngle >= otherAngle + 30) return;

        //сохранить сегмент, в который мы попали
        _other = other;
        OnOneTriggerEnter(other);
    }

    /// <summary>
    /// Выход из триггера.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other == null || !other.Equals(_other)) return;
        _other = null;
        OnOneTriggerExit(other);
    }

    /// <summary>
    /// Попадание в коллайдер.
    /// </summary>
    protected virtual void OnOneTriggerEnter(Collider other)
    {
        Debug.Log($"Бамм! {other.gameObject.name}");
    }

    /// <summary>
    /// Выход из коллайдера.
    /// </summary>
    protected virtual void OnOneTriggerExit(Collider other)
    {
        Debug.Log($"Чпок! {other.gameObject.name}");
    }
}
