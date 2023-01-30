using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Скрипт кляксы.
/// </summary>
public class Blot : MonoBehaviour
{
    /// <summary>
    /// Трансформа фигуры кляксы.
    /// </summary>
    [SerializeField] Transform _transform;

    /// <summary>
    /// Цвет фигуры кляксы.
    /// </summary>
    [SerializeField] Renderer _renderer;

    /// <summary>
    /// Вращение трансформы кляксы на случайный угол.
    /// </summary>
    public void Setup(Color color)
    {
        _transform.Rotate(0, Random.Range(0, 360), 0);

        _renderer.material.SetColor("_Color", color);
    }
}
