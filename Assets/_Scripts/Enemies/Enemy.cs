using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private LootData _lootData;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private int _powerReward;
    [SerializeField] protected float AggroDistance;

    public LootData LootData => _lootData; 

    private GameObject _target;

    private IDiyingPolicity diyingPolicity = new NormalDiyingPolicity();  

    public int PowerReward => _powerReward;
    public GameObject Target => _target;

    public event UnityAction<Enemy> Dying;
    public event UnityAction<Enemy> HealthEnded;

    public void Initialization(GameObject target)
    {
        _target = target;

    }

    public void TakeDamage(int damage)
    {
        if (_health >= damage)
        {
            _health -= damage;
        }
        else _health = 0;

        if (_health > 0)
        {
            StartCoroutine(HitCorutine());
        }

        if (IsDead())
        {
            HealthEnded?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return diyingPolicity.Died(_health);
    }

    private void OnDestroy()
    {
        Dying?.Invoke(this);
    }

    private IEnumerator HitCorutine()
    {
        yield return transform.DOScale(Vector3.one * (transform.localScale.x * 1.15f), 0.04f)
            .SetEase(Ease.OutBack)
            .SetLoops(2, LoopType.Yoyo)
            .WaitForCompletion();
    }
}