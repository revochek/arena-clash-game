using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlueEnemy : Enemy
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _aggroShootDistance;
    [SerializeField] private float _radiusProjectileSpawn;
    protected float _newDestinationCooldown = 0.3f;

    [Header("Shooting Settings:")]
    [Space]
    [SerializeField] private BlueEnemyProjectile _projectilePrefab;
    [SerializeField] private float _shootingInterval;
    [SerializeField] private float _speed;

    private float timeSinceLastShot;

    private void Start()
    {
        _navMeshAgent.speed = _speed;
    }
    private void Update()
    {
        if (Target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, Target.transform.position);

            if (distanceToTarget > AggroDistance)
            {
                _navMeshAgent.isStopped = false;
                TryMove();
            }
            else 
            {
                _navMeshAgent.isStopped = true;
            }

            if (distanceToTarget < _aggroShootDistance)
            {
                StartShooting();
            }
        }
    }
    public void TryMove()
    {
        if (_newDestinationCooldown <= 0)
        {
            _newDestinationCooldown = 0.3f;
            _navMeshAgent.SetDestination(Target.transform.position);
        }
        _newDestinationCooldown -= Time.deltaTime;
    }

    private void StartShooting()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= _shootingInterval)
        {
            ShootHero();
            timeSinceLastShot = 0f;
        }
    }

    private void ShootHero()
    {
        Vector3 directionToTarget = (Target.transform.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + directionToTarget * _radiusProjectileSpawn;

        BlueEnemyProjectile projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);

        if (projectile != null)
        {
            projectile.Initialization(Target);
        }
    }
}