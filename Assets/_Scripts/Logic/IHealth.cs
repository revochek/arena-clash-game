using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHealth
{
    int Current { get; }
    int Max { get; }
    int CriticalHealth { get; }

    event UnityAction<int, int> HealthChanged;
    event UnityAction Dying;

    void TakeDamage(int damage);

}