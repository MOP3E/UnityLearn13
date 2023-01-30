using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : BallEvents
{
    /// <summary>
    /// Текущий уровень.
    /// </summary>
    private int _currentLevel;

    /// <summary>
    /// Текущий уровень.
    /// </summary>
    public int CurrentLevel => _currentLevel;

    /// <summary>
    /// Загрузка сохранённого номера уровня.
    /// </summary>
    protected override void Awake()
    {
        Load();
        base.Awake();
    }

#if UNITY_EDITOR
    /// <summary>
    /// ОТЛАДКА: Обнуление всего по кнопке F1.
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) Reset();
    }
#endif

    /// <summary>
    /// Изменение уровня при столкновении мяча мяча с сегментом.
    /// </summary>
    protected override void OnCollisionWithSegment(SegmentType segmentType, Collider other)
    {
        switch (segmentType)
        {
            case SegmentType.Trap:
                //при попадании в ловушку возврат к первому уровню
                _currentLevel = 1;
                Save();
                break;
            case SegmentType.Finish:
                //на финишном этаже увеличение уровня
                _currentLevel++;
                Save();
                break;
        }
    }

    /// <summary>
    /// Сохранение прогресса.
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetInt("LevelProgress:_currentLevel", _currentLevel);
    }

    /// <summary>
    /// Загрузка прогресса.
    /// </summary>
    private void Load()
    {
        _currentLevel = PlayerPrefs.GetInt("LevelProgress:_currentLevel", 1);
    }

#if UNITY_EDITOR
    /// <summary>
    /// ОТЛАДКА: Обнуление всего.
    /// </summary>
    private void Reset()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endif
}
