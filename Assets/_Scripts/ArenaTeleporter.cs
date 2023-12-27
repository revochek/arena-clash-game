using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ArenaTeleporter : MonoBehaviour
{
    [SerializeField]
    private float _startMaxSpawnDistanceToEnemy;
    [SerializeField]
    private float _limitPossibleSpawnRadius;

    private float _initialMaxSpawnDistanceToEnemy;
    private const float _iterationStepDistance = 0.5f;

    private void Start()
    {
        _initialMaxSpawnDistanceToEnemy = _startMaxSpawnDistanceToEnemy;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Hero hero))
        {
            TeleportHeroToRandomPosition(hero);
        }
    }

    private void TeleportHeroToRandomPosition(Hero hero)
    {
        Vector3 randomPosition = GetRandomPositionOutsideEnemyRadius();
        randomPosition.y = 2f;
        hero.GetComponent<HeroMovement>().ResetAngularVelocity();
        hero.transform.position = randomPosition;
    }

    private Vector3 GetRandomPositionOutsideEnemyRadius()
    {
        Vector3[] enemyPositions = EnemyManager.instance.GetAllEnemiesPositions();

        Vector3 randomPosition;
        do
        {
            randomPosition = GetRandomPositionOnNavMeshSurface();
            _startMaxSpawnDistanceToEnemy -= _iterationStepDistance;
        } while (IsTooCloseToEnemies(randomPosition, enemyPositions));

        _startMaxSpawnDistanceToEnemy = _initialMaxSpawnDistanceToEnemy;
        return randomPosition;
    }

    private Vector3 GetRandomPositionOnNavMeshSurface()
    {
        NavMeshHit hit;

        Vector2 randomDirection = Random.insideUnitCircle * _limitPossibleSpawnRadius;
        Vector3 randomPosition = transform.position + new Vector3(randomDirection.x, 0f, randomDirection.y);

        if (NavMesh.SamplePosition(randomPosition, out hit, _limitPossibleSpawnRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }

    private bool IsTooCloseToEnemies(Vector3 position, Vector3[] enemyPositions)
    {
        for (int i = 0; i < enemyPositions.Length; i++)
        {
            if (Vector3.Distance(position, enemyPositions[i]) < _startMaxSpawnDistanceToEnemy)
            {
                return true;           
            }
        }

        return false;
    }
}