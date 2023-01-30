using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Движение мяча.
/// </summary>
public class BallMovement : MonoBehaviour
{
    /// <summary>
    /// Аниматор мяча.
    /// </summary>
    [SerializeField] private Animator _ballAnimator;

    /// <summary>
    /// Скорость падения мяча.
    /// </summary>
    [SerializeField] private float _fallSpeed;

    /// <summary>
    /// Высота пола, до которого нужно упасть.
    /// </summary>
    private float _floorY;

    /// <summary>
    /// Высота пола этажа по оси Y.
    /// </summary>
    [SerializeField] private float _floorHeight;

    /// <summary>
    /// Начальная настройка переменных.
    /// </summary>
    void Start()
    {
        enabled = false;
    }

    /// <summary>
    /// Перемещение мячика вниз.
    /// </summary>
    void Update()
    {
        if(transform.position.y > _floorY)
        {
            //transform.Translate(0, -_fallSpeed * Time.deltaTime, 0);
            transform.position += Vector3.down * _fallSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, _floorY, transform.position.z);
            enabled = false;
        }
    }

    /// <summary>
    /// Запуск прыжков мяча.
    /// </summary>
    public void Jump()
    {
        _ballAnimator.speed = 1;
    }

    /// <summary>
    /// Падение мяча на этаж ниже.
    /// </summary>
    /// <param name="currentFloor">Высота текущего этажа, с которого нужно упасть на один этаж ниже.</param>
    public void Fall(float currentFloor)
    {
        _ballAnimator.speed = 0;
        _floorY = currentFloor - _floorHeight;
        enabled = true;
    }

    /// <summary>
    /// Остановка прыжков мяча.
    /// </summary>
    public void Stop()
    {
        _ballAnimator.speed = 0;
    }
}
