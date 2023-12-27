using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private EnemySpawner EnemySpawner;

    private List<Enemy> _enemies = new List<Enemy>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        EnemySpawner.EnemyAdded += OnEnemyAdded;
        EnemySpawner.EnemyRemoved += OnEnemyRemoved;
    }

    private void OnDisable()
    {
        EnemySpawner.EnemyAdded -= OnEnemyAdded;
        EnemySpawner.EnemyRemoved -= OnEnemyRemoved;
    }

    public int GetEnemyCount() => _enemies.Count;

    public void DestroyAllEnemies()
    {
        if (_enemies.Count != 0)
        {
            foreach (Enemy enemy in _enemies)
            {
                Destroy(enemy.gameObject);
            }
        }

        _enemies = new List<Enemy>();
    }

    public Vector3 FindClosestEnemyPosition(Vector3 point)
    {
        Vector3 closestPosition = Vector3.zero;
        float closestDistance = float.MaxValue;

        Vector3[] enemyPositions = GetAllEnemiesPositions();

        foreach (Vector3 enemyPosition in enemyPositions)
        {
            float distance = Vector3.Distance(point, enemyPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = enemyPosition;
            }           
        }
       
        return closestPosition;
    }
    public Vector3[] GetAllEnemiesPositions()
    {
        List<Vector3> enemyPositions = new List<Vector3>();

        foreach (Enemy enemy in _enemies)
        {
            if (!enemy.IsDead())
            {
                enemyPositions.Add(enemy.transform.position);
            }
        }

        return enemyPositions.ToArray();
    }
    private void OnEnemyAdded(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    private void OnEnemyRemoved(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

}