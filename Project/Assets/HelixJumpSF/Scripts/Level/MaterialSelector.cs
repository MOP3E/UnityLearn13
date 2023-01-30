using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelector : MonoBehaviour
{
    /// <summary>
    /// Все цвета уровня
    /// </summary>
    [SerializeField] private List<Material> _allMaterials;

    /// <summary>
    /// Индекс выбранных цветов уровня.
    /// </summary>
    private int _materialIndex;

    /// <summary>
    /// Материал для оси уровня.
    /// </summary>
    public Material Axis => _allMaterials[_materialIndex * 5];

    /// <summary>
    /// Материал для мяча.
    /// </summary>
    public Material Ball => _allMaterials[_materialIndex * 5 + 1];

    /// <summary>
    /// Материал для обычного сегмента.
    /// </summary>
    public Material Default => _allMaterials[_materialIndex * 5 + 2];

    /// <summary>
    /// Материал для финишного сегмента.
    /// </summary>
    public Material Finish => _allMaterials[_materialIndex * 5 + 3];

    /// <summary>
    /// Материал для ловушки.
    /// </summary>
    public Material Trap => _allMaterials[_materialIndex * 5 + 4];

    /// <summary>
    /// Выбор набора материалов для отображения уровня.
    /// </summary>
    private void Awake()
    {
        //новый набор материалов не должен совпадать со старым
        Load();
        while (true)
        {
            //в каждом наборе по 5 материалов, наборы идут подряд друг за другом
            int materialIndex = Random.Range(0, _allMaterials.Count / 5);
            if (materialIndex != _materialIndex)
            {
                _materialIndex = materialIndex;
                break;
            }
        }
        Save();
    }

    /// <summary>
    /// Сохранение прогресса.
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetInt("MaterialSelector:_materialIndex", _materialIndex);
    }

    /// <summary>
    /// Загрузка прогресса.
    /// </summary>
    private void Load()
    {
        _materialIndex = PlayerPrefs.GetInt("MaterialSelector:_materialIndex", int.MinValue);
    }
}
