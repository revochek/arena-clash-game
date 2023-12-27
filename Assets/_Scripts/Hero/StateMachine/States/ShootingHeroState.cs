using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingHeroState : HeroState
{
    [SerializeField]
    private HeroShooter heroShooter;

    public override void Enter()
    {
        base.Enter();
        heroShooter.Shoot();
        stateMachine.ChangeState(stateMachine.Standing);
    }
}