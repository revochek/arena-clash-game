using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingHeroState : HeroState
{
    [SerializeField] private HeroHealth _health;

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        transform.DOScale(Vector3.zero, 1.2f);

        yield return new WaitForSecondsRealtime(1.5f);

        _health.TriggerDyingEvent();
    }
}
