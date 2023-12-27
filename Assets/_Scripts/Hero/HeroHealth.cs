using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class HeroHealth : MonoBehaviour, IHealth
{
    private int _max;
    private int _ñriticalHealth;
    private int _current;

    public int Current { get => _current; }
    public int Max { get => _max; }
    public int CriticalHealth { get => _ñriticalHealth; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Dying;

    private IDiyingPolicity diyingPolicity;

    public void Initialization(HeroData heroData, IDiyingPolicity diyingPolicity)
    {
        _current = heroData.StartHealth;
        _max = heroData.MaxHealth;
        _ñriticalHealth = heroData.CriticalHealth;

        this.diyingPolicity = diyingPolicity;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_current, _max);
    }

    public void TakeDamage(int damage)
    {
        if (_current >= damage)
        {
            _current -= damage;
            StartCoroutine(HitCorutine());                   
        }
        else _current = 0;

        HealthChanged?.Invoke(_current, _max);
    }

    public bool IsDead()
    {
        return diyingPolicity.Died(_current);
    }

    public bool IsHealthCritical()
    {
        return _ñriticalHealth >= _current;
    }

    public void TriggerDyingEvent()
    {
        Dying?.Invoke();
    }

    public void AccrueLootReward(LootData lootable)
    {
        if (lootable.Health != 0)
        {
            _current = Mathf.Clamp(_current + lootable.Health, 0, _max);
            HealthChanged?.Invoke(_current, _max);
        }
    }

    public void IncreaseHealthByHalf()
    {
        _current = Mathf.Clamp(_current + (_current / 2), 0, _max);
        HealthChanged?.Invoke(_current, _max);
    }

    private IEnumerator HitCorutine()
    {
        yield return transform.DOScale(Vector3.one * (transform.localScale.x * 1.2f), 0.05f).SetEase(Ease.OutBack)
            .SetLoops(2, LoopType.Yoyo)
            .WaitForCompletion();
    }
}