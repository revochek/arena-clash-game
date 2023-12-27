using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroProjectile : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _extraChance = 1f;
    [SerializeField] private LootData _ricochetLootData;
    private HeroHealth _health;

    public LootData RicochetLootData => _ricochetLootData;

    private Vector3 _direction;
    private bool _isRicochetChanceProjectile;
    private bool _isThroughChanceProjectile;

    public event UnityAction<HeroProjectile> RicochetKilled;

    public void Initialization(HeroHealth health)
    {
        _health = health;
    }

    private void Start()
    {
        UpdateProjectileDirection();
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        MoveProjectile();
    }

    private void UpdateProjectileDirection()
    {
        _direction = FindDirectionToNewTarget(EnemyManager.instance.FindClosestEnemyPosition(transform.position), transform.position);
    }

    private void MoveProjectile()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        HandleDamageCollision(collision);
        HandleEnemyCollision(collision);
    }

    private void HandleDamageCollision(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable) && !collision.gameObject.TryGetComponent(out Hero hero))
        {
            damageable.TakeDamage(_damage);
        }
    }

    private void HandleEnemyCollision(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (!_isRicochetChanceProjectile && !_isThroughChanceProjectile)
            {
                HandleStandardProjectileCollision(enemy);
            }
            else
            {
                HandleSpecialProjectileCollision();
            }
        }
    }

    private void HandleStandardProjectileCollision(Enemy enemy)
    {
        if (enemy.IsDead())
        {
            HandleDeadEnemyCollision();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void HandleDeadEnemyCollision()
    {
        if (Random.value <= _extraChance || _health.IsHealthCritical())
        {
            DetermineSpecialProjectileType();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DetermineSpecialProjectileType()
    {
        if (Random.value < 0.5f)
        {
            UpdateProjectileDirection();
            _isRicochetChanceProjectile = true;
        }
        else
        {
            _isThroughChanceProjectile = true;
        }
    }

    private Vector3 FindDirectionToNewTarget(Vector3 originPosition, Vector3 targetPosition)
    {
        return (originPosition - targetPosition).normalized;
    }

    private void HandleSpecialProjectileCollision()
    {
        RicochetKilled?.Invoke(this);
        Destroy(gameObject);
    }

}