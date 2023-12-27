using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScreen : Screen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    public event UnityAction RestartButtonClick;
    public event UnityAction ExitButtonClick;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestarButtonClick);
        _exitButton.onClick.AddListener(OnRestarButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestarButtonClick);
        _exitButton.onClick.AddListener(OnRestarButtonClick);
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

    private void OnRestarButtonClick()
    {
        RestartButtonClick?.Invoke();
    }
    private void OnExitButtonClick()
    {
        ExitButtonClick?.Invoke();
    }

}
