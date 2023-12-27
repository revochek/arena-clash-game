using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroMovement))]
[RequireComponent(typeof(HeroStateMachine))]
public class HeroStateMachine : MonoBehaviour
{
    [SerializeField] 
    private HeroData _data;
    [SerializeField] 
    private StandingHeroState _standing;
    [SerializeField] 
    private ShootingHeroState _shooting;
    [SerializeField] 
    private UltimateHeroState _ultimateAttack;
    [SerializeField]
    private DyingHeroState _dying;

    private HeroState _currentState;

    public StandingHeroState Standing => _standing;
    public ShootingHeroState BasicAttack => _shooting;
    public UltimateHeroState UltimateAttack => _ultimateAttack;
    public DyingHeroState Dying => _dying;

    public void Initialize(HeroState startState)
    {
        InitializeStates();
        _currentState = startState;
        _currentState.Enter();
    }

    public void InitializeStates()
    {
        _standing.Initialize(this, _data);
        _shooting.Initialize(this, _data);
        _ultimateAttack.Initialize(this, _data);
        _dying.Initialize(this, _data);
    }

    public void ChangeState(HeroState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState.HandleInput();
        _currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.PhysicsUpdate();
    }
}