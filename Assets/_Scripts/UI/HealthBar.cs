using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private HeroHealth _health;

    private void OnEnable()
    {
        _health.HealthChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnValueChanged;
    }
}
