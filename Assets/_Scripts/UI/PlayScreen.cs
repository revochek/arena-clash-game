using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class PlayScreen : Screen
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    public event UnityAction PauseButtonClick;
    public event UnityAction RestartButtonClick;
    public event UnityAction ExitButtonClick;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _restartButton.onClick.AddListener(OnRestarButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _restartButton.onClick.RemoveListener(OnRestarButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }


    public override void Close()
    {
        CanvasGroup.alpha = 0;
        Deactivate();
    }

    public override void Open()
    {
        CanvasGroup.DOFade(1f, 0.2f).SetUpdate(true);
        Activate();
    }

    private void OnPauseButtonClick()
    {
        PauseButtonClick?.Invoke();
    }
    private void OnRestarButtonClick()
    {
        RestartButtonClick?.Invoke();
    }
    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }

}
