using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// След в виде кляксы.
/// </summary>
public class BlotTrack : BallEvents
{
    /// <summary>
    /// Трансформа уровня.
    /// </summary>
    [SerializeField] private Transform _level;

    /// <summary>
    /// Трансформа мяча.
    /// </summary>
    [SerializeField] private Transform _ball;

    /// <summary>
    /// Растеризатор мячика.
    /// </summary>
    [SerializeField] private Renderer _ballRenderer;

    /// <summary>
    /// Растеризатор мячика.
    /// </summary>
    private Color _ballColor;

    /// <summary>
    /// Цвет получен.
    /// </summary>
    private bool _colorReaded = false;
    
    /// <summary>
    /// Префаб кляксы.
    /// </summary>
    [SerializeField] private Blot _blotPrefab;

    /// <summary>
    /// Список клякс.
    /// </summary>
    private List<Blot> _blots = new List<Blot>();

    /// <summary>
    /// Генерация кляксы после столкновения с сегментом.
    /// </summary>
    /// <param name="segmentType"></param>
    protected override void OnCollisionWithSegment(SegmentType segmentType, Collider other)
    {
        if (segmentType != SegmentType.Empty)
        {
            if(!_colorReaded)
            {
                _ballColor = _ballRenderer.material.GetColor("_Color");
            }

            Blot blot = Instantiate(_blotPrefab, transform);
            blot.Setup(_ballColor);
            blot.transform.Translate(0, transform.position.y, 0);
            blot.transform.SetParent(_level, false);
            //коэффициенты для поворота подобраны эмпирически:
            //почему-то угол поворота уровня относительно кляксы отрицательный, да ещё и сдвинут на 90 градусов
            blot.transform.Rotate(0, -_level.eulerAngles.y - 90, 0);
            _blots.Add(blot);
        }
        else
        {
            //падение в дырку - нужно уничтожить кляксы и взорвать этаж
            foreach (Blot blot in _blots) Destroy(blot.gameObject);
            _blots.Clear();
            other.GetComponentInParent<Floor>().Explode();
        }
    }
}
