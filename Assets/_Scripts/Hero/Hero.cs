using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroStateMachine _stateMachine;
    [SerializeField] private HeroData _data;

    [SerializeField] private HeroHealth _heroHealth;

    private int _maxPower;
    private int _powerToPay;
    private int _power;
    public int Power => _power;

    public event UnityAction<int, int> PowerChanged;

    public void Initialize()
    {
        _heroHealth.Initialization(_data, new NormalDiyingPolicity());

        _maxPower = _data.MaxPower;
        _power = _data.StartPower;              

        _stateMachine.Initialize(_stateMachine.Standing);
    }

    private void Start()
    {
        PowerChanged?.Invoke(_power, _maxPower);
    }

    public void AccrueLootReward(LootData lootable)
    {
        _heroHealth.AccrueLootReward(lootable);

        if (lootable.Power != 0)
        {
            _power = Mathf.Clamp(_power + lootable.Power, 0, _maxPower);
            PowerChanged?.Invoke(_power, _maxPower);
        }
    }

    public bool CheckSolvencyPower(int powerPrice)
    {
        _powerToPay = powerPrice;
        if (_power >= _maxPower)
        {
            return true;
        }
        else
        {
            _powerToPay = 0;
            return false;
        }
    }

    public void ToPayPower()
    {
        _power -= _powerToPay;
        PowerChanged?.Invoke(_power, _maxPower);
    }
}
