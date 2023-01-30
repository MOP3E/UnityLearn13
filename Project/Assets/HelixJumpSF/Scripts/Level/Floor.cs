using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floor : MonoBehaviour
{
    /// <summary>
    /// Стандартные сегменты этажа.
    /// </summary>
    [SerializeField] private List<Segment> _defaultSegments;

    /// <summary>
    /// Все остальные сегменты этажа.
    /// </summary>
    private List<Segment> _otherSegments = new List<Segment>();

    /// <summary>
    /// Скорость взрыва уровня.
    /// </summary>
    private const float ExplodeSpeed = 10;

    /// <summary>
    /// Дистанция до удаления сегментов.
    /// </summary>
    private const float ExplodeDistance = 10;

    /// <summary>
    /// Уровень взрывается.
    /// </summary>
    private bool _explode = false;

    /// <summary>
    /// Дистанция, пройденная сегментами.
    /// </summary>
    private float _explodeDistance = 0f;

    /// <summary>
    /// Добавить пустые сегменты. Вызывать первым и один раз!
    /// </summary>
    /// <param name="count">Ширина дырки.</param>
    public void AddEmptySegment(int count)
    {
        //пометить сегменты как пустые
        for (int i = 0; i < count; i++)
        {
            _defaultSegments[i].SetType(SegmentType.Empty);
        }

        //удалить пустые сегменты из списка стандартных сегментов
        for (int i = _defaultSegments.Count - 1; i >= 0; i--)
        {
            if (_defaultSegments[i].SegmentType == SegmentType.Empty)
            {
                _otherSegments.Add(_defaultSegments[i]);
                _defaultSegments.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Добавить ловушки в случайных местах.
    /// </summary>
    /// <param name="count">Число ловушек.</param>
    public void AddRandomTrapSegment(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, _defaultSegments.Count);

            _defaultSegments[index].SetType(SegmentType.Trap);
            _otherSegments.Add(_defaultSegments[index]);
            _defaultSegments.RemoveAt(index);
        }
    }

    /// <summary>
    /// Поворот уровня на случайный угол.
    /// </summary>
    public void SetRandomRotation()
    {
        //развернуть все сегменты уровня на случайный угол
        int angle = Random.Range(0, 360);
        //разворот стандартных сегментов
        foreach (Segment defaultSegment in _defaultSegments) 
            defaultSegment.transform.eulerAngles = new Vector3(0, defaultSegment.transform.eulerAngles.y + angle, 0);
        //разворот всех остальных сегментов
        foreach (Segment defaultSegment in _otherSegments) 
            defaultSegment.transform.eulerAngles = new Vector3(0, defaultSegment.transform.eulerAngles.y + angle, 0);
    }

    /// <summary>
    /// Сделать этаж финишным.
    /// </summary>
    public void SetFinishFloor()
    {
        foreach (Segment segment in _defaultSegments) 
            segment.SetType(SegmentType.Finish);
        _otherSegments.AddRange(_defaultSegments);
        _defaultSegments.Clear();
    }

    /// <summary>
    /// Задать сегментам материалы.
    /// </summary>
    /// <param name="materialSelector"></param>
    public void SetMaterials(MaterialSelector materialSelector)
    {
        foreach (Segment segment in _defaultSegments)
        {
            segment.SetMaterial(materialSelector.Default);
        }

        foreach (Segment segment in _otherSegments)
        {
            switch (segment.SegmentType)
            {
                case SegmentType.Default:
                    segment.SetMaterial(materialSelector.Default);
                    break;
                case SegmentType.Trap:
                    segment.SetMaterial(materialSelector.Trap);
                    break;
                case SegmentType.Finish:
                    segment.SetMaterial(materialSelector.Finish);
                    break;
            }
        }
    }

    /// <summary>
    /// Взорвать сегменты уровня.
    /// </summary>
    public void Explode()
    {
        //запустить взрыв этажа
        _explode = true;
    }

    /// <summary>
    /// Взрыв этажа.
    /// </summary>
    private void Update()
    {
        if (!_explode) return;

        //раздвигать сегменты в стороны
        foreach (Segment segment in _defaultSegments) segment.transform.Translate(new Vector3(0, 0, ExplodeSpeed * Time.deltaTime));
        foreach (Segment segment in _otherSegments) segment.transform.Translate(new Vector3(0, 0, ExplodeSpeed * Time.deltaTime));

        //по достижении дистанции разбегания уничтожить сегменты и остановить взрыв этажа
        _explodeDistance += ExplodeSpeed * Time.deltaTime;
        if (_explodeDistance < ExplodeDistance) return;
        foreach (Segment segment in _defaultSegments) Destroy(segment.gameObject);
        _defaultSegments.Clear();
        foreach (Segment segment in _otherSegments) Destroy(segment.gameObject);
        _otherSegments.Clear();
        _explode = false;
    }
}
