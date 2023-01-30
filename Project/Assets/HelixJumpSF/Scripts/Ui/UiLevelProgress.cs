using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLevelProgress : MonoBehaviour
{
    /// <summary>
    /// Контроллер мяча.
    /// </summary>
    [SerializeField] internal BallController _ballController;

    /// <summary>
    /// Генератор уровней.
    /// </summary>
    [SerializeField] private LevelGenerator _levelGenerator;

    /// <summary>
    /// Прогресс игрока.
    /// </summary>
    [SerializeField] private LevelProgress _levelProgress;

    /// <summary>
    /// Номер текущего уровня.
    /// </summary>
    [SerializeField] private Text _currentLevelText;

    /// <summary>
    /// Номер следующего уровня.
    /// </summary>
    [SerializeField] private Text _nextLevelText;

    /// <summary>
    /// Прогресс прохождения уровня.
    /// </summary>
    [SerializeField] private Image _progressBar;

    /// <summary>
    /// Предыдущее значение высоты мяча на уровне.
    /// </summary>
    private float _previousBallY;

    /// <summary>
    /// Высота мяча на уровне изменилась.
    /// </summary>
    private bool _ballYchanged;

    /// <summary>
    /// Инициализация текстовых полей.
    /// </summary>
    private void Start()
    {
        _currentLevelText.text = _levelProgress.CurrentLevel.ToString();
        _nextLevelText.text = (_levelProgress.CurrentLevel + 1).ToString();

        _progressBar.fillAmount = 0;
        _previousBallY = 0;
        _ballYchanged = false;
    }

    /// <summary>
    /// Перерисовка прогресс-бара при изменении положения мяча.
    /// </summary>
    private void Update()
    {
        //мяч меняет положение - запомнить, что положение изменилось
        if(_previousBallY != _ballController.transform.position.y)
        {
            _previousBallY = _ballController.transform.position.y;
            _ballYchanged = true;
            return;
        }

        //мяч перестал менять положение - перерисовать прогресс-бар и забыть, что положение изменилось
        if(_previousBallY == _ballController.transform.position.y && _ballYchanged)
        {
            _progressBar.fillAmount = (_levelGenerator.LastFloorY - _previousBallY) / _levelGenerator.LastFloorY;
            _ballYchanged = false;
            return;
        }
    }
}
