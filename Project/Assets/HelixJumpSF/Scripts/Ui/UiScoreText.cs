using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScoreText : MonoBehaviour
{
    /// <summary>
    /// Скрипт подсчёта очков игрока.
    /// </summary>
    [SerializeField] private ScoresCollector _scoresCollector;

    /// <summary>
    /// Текст для вывода очков на экран.
    /// </summary>
    [SerializeField] private Text _scoreText;

    /// <summary>
    /// Предыдущие очки игрока.
    /// </summary>
    private int _prevScore = int.MinValue;

    /// <summary>
    /// Текст для вывода рекорда на экран.
    /// </summary>
    [SerializeField] private Text _recordText;

    /// <summary>
    /// Предыдущий рекорд игрока.
    /// </summary>
    private int _prevRecord = int.MinValue;

    /// <summary>
    /// Вывод на экран очков игрока.
    /// </summary>
    private void Update()
    {
        //вывод на экран очков игрока
        if (_prevScore != _scoresCollector.Score)
        {
            _scoreText.text = _scoresCollector.Score.ToString();
            _prevScore = _scoresCollector.Score;
        }

        //вывод на экран рекорда
        if (_prevRecord != _scoresCollector.Score)
        {
            _recordText.text = $"Рекорд {_scoresCollector.Record}";
            _prevRecord = _scoresCollector.Record;
        }
    }
}
