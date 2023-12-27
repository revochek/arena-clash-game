using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingHeroState : HeroState
{
    [SerializeField]
    private HeroMovement _movement;
    [SerializeField]
    private HeroHealth _health;

    private bool _isBasicAttack;
    private bool _isUltimateAttack;

    public override void Enter()
    {
        base.Enter();
        _isBasicAttack = false;
        _isUltimateAttack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _isBasicAttack = InputManager.instance.IsBasicAttackWasPressedThisFrame();
        _isUltimateAttack = InputManager.instance.IsUltimateAttackWasPressedThisFrame();

        if(_health.IsDead())
        {
            stateMachine.ChangeState(stateMachine.Dying);
        }

        if(_isBasicAttack)
        {
            stateMachine.ChangeState(stateMachine.BasicAttack);
        }

        if (_isUltimateAttack)
        {
            stateMachine.ChangeState(stateMachine.UltimateAttack);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _movement.Move(InputManager.instance.GetMoveDirection(), data.MovementSpeed);
    }
}
