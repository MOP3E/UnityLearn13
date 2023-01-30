using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Вспомогательные методы для управления игровым уровнем.
/// </summary>
public class UiGamePanel : BallEvents
{
    /// <summary>
    /// Сообщение об успешном прохождении уровня.
    /// </summary>
    [SerializeField] private GameObject _passedPanel;

    /// <summary>
    /// Сообщение о проигрыше.
    /// </summary>
    [SerializeField] private GameObject _defeatPanel;

    /// <summary>
    /// Скрытие панелей перед запуском.
    /// </summary>
    private void Start()
    {
        _passedPanel.SetActive(false);
        _defeatPanel.SetActive(false);
    }

    /// <summary>
    /// Включение панелей при событии.
    /// </summary>
    protected override void OnCollisionWithSegment(SegmentType segmentType, Collider other)
    {
                switch (segmentType)
        {
            case SegmentType.Trap:
                _defeatPanel.SetActive(true);
                break;
            case SegmentType.Finish:
                _passedPanel.SetActive(true);
                break;
        }
    }
}
