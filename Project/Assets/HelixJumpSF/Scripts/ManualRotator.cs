using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ручное вращение уровня при помощи мыши либо клавиатуры.
/// </summary>
public class ManualRotator : MonoBehaviour
{
    /// <summary>
    /// Ось ввода мыши.
    /// </summary>
    [SerializeField] private string _mouseInputAxis;

    /// <summary>
    /// Ось ввода клавиатуры.
    /// </summary>
    [SerializeField] private string _keyboardInputAxis;

    /// <summary>
    /// Чувствительность при повороте.
    /// </summary>
    [SerializeField] private float _sensitivity;

    /// <summary>
    /// Чувствительность при повороте клавиатурой.
    /// </summary>
    private const float _keyboardSensitivityFactor = 25f;

    /// <summary>
    /// Вращение уровня при помощи мыши.
    /// </summary>
    void Update()
    {
        if (!string.IsNullOrEmpty(_mouseInputAxis))
        {
            if (Input.GetMouseButton(0))
            {
                transform.Rotate(0, Input.GetAxis(_mouseInputAxis) * -_sensitivity, 0);
            }
        }
        if (!string.IsNullOrEmpty(_keyboardInputAxis))
        {
            
            transform.Rotate(0, Input.GetAxis(_keyboardInputAxis) * -_sensitivity * _keyboardSensitivityFactor * Time.deltaTime, 0);
        }
    }
}
