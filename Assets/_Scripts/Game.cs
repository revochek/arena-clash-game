using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private PlayScreen _playScreen;
    [SerializeField] private PauseScreen _pauseScreen;
    private HeroHealth _health;

    private bool _isPause;
    private float originalTimeScale;

    public void Initialization(HeroHealth health)
    {
        _health = health;
        _health.Dying += OnGameOver;
    }

    private void OnEnable()
    {
        _playScreen.PauseButtonClick += OnPauseButtonClick;
        _playScreen.RestartButtonClick += OnRestarButtonClick;
        _playScreen.ExitButtonClick += OnExitButtonClick;

        _gameOverScreen.RestartButtonClick += OnRestarButtonClick;
        _gameOverScreen.ExitButtonClick += OnExitButtonClick;
    }

    private void OnDisable()
    {
        _health.Dying -= OnGameOver;
    }

    private void Start()
    {
        originalTimeScale = 1;
        Time.timeScale = originalTimeScale;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }

    private void OnPauseButtonClick()
    {
        _isPause = !_isPause;

        if (_isPause)
        {
            originalTimeScale = Time.timeScale;
            _pauseScreen.Open();
            Time.timeScale = 0;
        }
        else
        {
            _pauseScreen.Close();
            Time.timeScale = originalTimeScale; 
        }
    }

    private void OnRestarButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
