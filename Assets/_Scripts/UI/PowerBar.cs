using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerBar : Bar
{
    [SerializeField] private Hero _hero;

    private void OnEnable()
    {
        _hero.PowerChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _hero.PowerChanged -= OnValueChanged;
    }
}
