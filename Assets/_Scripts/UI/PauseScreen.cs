using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : Screen
{
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
}
