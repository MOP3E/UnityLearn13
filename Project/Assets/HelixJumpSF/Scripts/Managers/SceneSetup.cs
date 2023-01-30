using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    /// <summary>
    /// Генератор уровней.
    /// </summary>
    [SerializeField] private LevelGenerator _levelGenerator;
    /// <summary>
    /// Контроллер мяча.
    /// </summary>
    [SerializeField] private BallController _ballController;
    /// <summary>
    /// Прогресс уровня.
    /// </summary>
    [SerializeField] private LevelProgress _levelProgress;    

    /// <summary>
    /// Настройка сцены.
    /// </summary>
    private void Start()
    {
        _levelGenerator.Generate(_levelProgress.CurrentLevel);
        _ballController.transform.position = new Vector3(_ballController.transform.position.x, _levelGenerator.LastFloorY, _ballController.transform.position.z);
    }
}
