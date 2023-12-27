using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateHeroState : HeroState
{
    [SerializeField]
    private HeroUltimate _heroUltimate;

    public override void Enter()
    {
        base.Enter();
        _heroUltimate.UseUltimate();
        stateMachine.ChangeState(stateMachine.Standing);
    }
}
