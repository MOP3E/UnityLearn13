using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresCollector : BallEvents
{
    /// <summary>
    /// Прогресс уровня.
    /// </summary>
    [SerializeField] private LevelProgress _levelProgress;

    /// <summary>
    /// Очки игрока.
    /// </summary>
    private int _score;

    /// <summary>
    /// Очки игрока.
    /// </summary>
    public int Score => _score;

    /// <summary>
    /// Рекорд.
    /// </summary>
    private int _record;

    /// <summary>
    /// Рекорд.
    /// </summary>
    public int Record => _record;

    /// <summary>
    /// Множитель пройденных подряд этажей.
    /// </summary>
    private int _floorMultiplier = 0;

    /// <summary>
    /// Загрузка сохранённыхо очков.
    /// </summary>
    protected override void Awake()
    {
        Load();
        base.Awake();
    }

    /// <summary>
    /// Подсчёт очков при столкновении мяча с сегментом.
    /// </summary>
    protected override void OnCollisionWithSegment(SegmentType segmentType, Collider other)
    {
        switch (segmentType)
        {
            case SegmentType.Default:
                _floorMultiplier = 0;
                break;
            case SegmentType.Trap:
                //при столкновении с ловушкой очки обнуляются
                if (_record < _score) _record = _score;
                //очки не обнуляются, чтобы игрок видел, сколько очков он набрал
                //вместо этого сохраняется 0 очков, которые будут загружены при перезапуске
                Save(0);
                _floorMultiplier = 0;
                break;
            case SegmentType.Empty:
                //при переходе на этаж ниже начисляются очки
                _score += _levelProgress.CurrentLevel + _floorMultiplier * _levelProgress.CurrentLevel;
                //в следующем попадании будет бонус, кратный пройденным подряд дыркам
                _floorMultiplier++;
                break;
            case SegmentType.Finish:
                //на финишном этаже очки сохраняются
                Save(_score);
                _floorMultiplier = 0;
                break;
        }
    }

    /// <summary>
    /// Сохранение очков.
    /// </summary>
    private void Save(int score)
    {
        PlayerPrefs.SetInt("ScoresCollector:_score", score);
        PlayerPrefs.SetInt("ScoresCollector:_record", _record);
    }

    /// <summary>
    /// Загрузка очков.
    /// </summary>
    private void Load()
    {
        _score = PlayerPrefs.GetInt("ScoresCollector:_score", 0);
        _record = PlayerPrefs.GetInt("ScoresCollector:_record", 0);
    }
}
