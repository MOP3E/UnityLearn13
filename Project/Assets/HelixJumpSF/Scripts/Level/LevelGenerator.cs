using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /// <summary>
    /// Растеризатор мяча.
    /// </summary>
    [Header("Объекты")]
    [SerializeField] MeshRenderer _ball;

    /// <summary>
    /// Трансформа оси уровня.
    /// </summary>
    [SerializeField] private Transform _levelAxis;

    /// <summary>
    /// Растеризатор оси уровня.
    /// </summary>
    private MeshRenderer _levelAxisRenderer;
    
    /// <summary>
    /// Игровой объект оси уровня.
    /// </summary>
    private GameObject _levelAxisGameObject;

    /// <summary>
    /// Префаб уровня.
    /// </summary>
    [SerializeField] private Floor _floorPrefab;

    /// <summary>
    /// Набор материалов для генерации уровня.
    /// </summary>
    [SerializeField] private MaterialSelector _materialSelector;

    /// <summary>
    /// Минимальное количество этажей на уровне.
    /// </summary>
    [Header("Настройки")] 
    [SerializeField] private int _minFloorsCount;

    /// <summary>
    /// Высота этажа.
    /// </summary>
    [SerializeField] private float _floorHeight;

    /// <summary>
    /// Число пустых сегментов на этаже (ширина люка).
    /// </summary>
    [SerializeField] private int _emptySegmentsCount;

    /// <summary>
    /// Минимальное количество ловушек на этаже.
    /// </summary>
    [SerializeField] private int _minTrapsCount;

    /// <summary>
    /// Максимальное количество ловушек на этаже.
    /// </summary>
    [SerializeField] private int _maxTrapsCount;

    /// <summary>
    /// Число этажей на текущем уровне.
    /// </summary>
    private int _floorsCount;

    /// <summary>
    /// Позиция верхнего этажа по Y.
    /// </summary>
    public float LastFloorY => (_floorsCount - 1) * _floorHeight;

    /// <summary>
    /// Генерация уровня.
    /// </summary>
    /// <param name="levelNumber">Номер генерируемого уровня.</param>
    public void Generate(int levelNumber)
    {
        //очистить уровень от всего лишнего
        DestroyChildren();

        //задать количество этажей на уровне
        _floorsCount = _minFloorsCount + levelNumber;

        //задать материал мяча
        _ball.material = _materialSelector.Ball;

        //задать высоту и материал оси уровня
        _levelAxis.transform.localScale = new Vector3(1, _floorsCount * _floorHeight, 1);
        if(_levelAxisRenderer == null) _levelAxisRenderer = _levelAxis.gameObject.GetComponent<MeshRenderer>();
        _levelAxisRenderer.material = _materialSelector.Axis;

        for (int i = 0; i < _floorsCount; i++)
        {
            Floor floor = Instantiate(_floorPrefab, transform);
            floor.transform.Translate(0, i * _floorHeight, 0);
            floor.name = $"Floor {i + 1}";

            if (i == 0)
            {
                //первый этаж всегда финишный
                floor.SetFinishFloor();
            }
            else if (i == _floorsCount - 1)
            {
                //верхний этаж без ловушек
                floor.AddEmptySegment(_emptySegmentsCount);
            }
            else
            {
                //все остальные этажи
                floor.AddEmptySegment(_emptySegmentsCount);
                floor.AddRandomTrapSegment(Random.Range(_minTrapsCount, _maxTrapsCount + 1));
                floor.SetRandomRotation();
            }

            //задать сегментам этажа материалы
            floor.SetMaterials(_materialSelector);
        }
    }

    /// <summary>
    /// Уничтожить всё на уровне, за исключением оси.
    /// </summary>
    private void DestroyChildren()
    {
        //получить игровой объект оси уровня
        if (_levelAxisGameObject == null) _levelAxisGameObject = _levelAxis.gameObject;

        //уничтожить все объекты кроме оси уровня
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if (go.Equals(_levelAxisGameObject)) continue;
            Destroy(go);
        }
    }
}
