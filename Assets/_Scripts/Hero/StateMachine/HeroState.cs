using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroState : MonoBehaviour
{
    protected HeroStateMachine stateMachine { get; private set; }
    protected HeroData data { get; private set; }

    public virtual void Initialize(HeroStateMachine stateMachine, HeroData data)
    {
        this.stateMachine = stateMachine;
        this.data = data;
    }

    public virtual void Enter() { }
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}