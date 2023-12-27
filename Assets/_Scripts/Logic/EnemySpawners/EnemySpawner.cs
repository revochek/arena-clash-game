using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnLocation _spawnLocation;

    [Header("Count Settings:")]
    [Space]
    [SerializeField] private int _currentEnemyLimit;
    [SerializeField] private int _maxEnemyLimit;

    [Header("Delay Settings:")]
    [Space]
    [SerializeField] private float _currentSpawnDelay;
    [SerializeField] private float _stepDecreaseDelay;
    [SerializeField] private float _minSpawnDelay;

    [SerializeField] private KillData _killData;

    private Hero _hero;
    private float _timeAfterLastSpawn;

    private RedEnemyFactory _redEnemyFactory;
    private  BlueEnemyFactory _blueEnemyFactory;

    public event UnityAction<Enemy> EnemyAdded;
    public event UnityAction<Enemy> EnemyRemoved;


    public void Initialization(Hero hero)
    {
        _hero = hero;

        _redEnemyFactory = new RedEnemyFactory();
        _blueEnemyFactory = new BlueEnemyFactory();

        _killData.Clear();
        InstantiateEnemy();
    }

    private void Update()
    {      
        _timeAfterLastSpawn += Time.deltaTime;
        int currentEnemyCount = EnemyManager.instance.GetEnemyCount();

        if (_timeAfterLastSpawn >= _currentSpawnDelay && _maxEnemyLimit > currentEnemyCount)
        {
            InstantiateEnemy();

            if(_currentSpawnDelay > _minSpawnDelay)
            {
                _currentSpawnDelay -= _stepDecreaseDelay;
            }
            else if (_maxEnemyLimit > _currentEnemyLimit)
            {
                _currentEnemyLimit++;
            }

            _timeAfterLastSpawn = 0;
        }
    }

    private void InstantiateEnemy()
    {
        int spawnRatio = 4;
        int randomValue = Random.Range(0, spawnRatio + 1);

        IEnemyFactory enemyFactory = randomValue == 0 ? _blueEnemyFactory : _redEnemyFactory;
        Enemy enemy = enemyFactory.CreateEnemy(_spawnLocation.GetSpawnPoint(), _spawnLocation.GetParentTransform());
        enemy.Initialization(_hero.gameObject);

        enemy.HealthEnded += OnEnemyHealthEnded;
        enemy.Dying += OnEnemyDying;
        EnemyAdded?.Invoke(enemy);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        EnemyRemoved?.Invoke(enemy);
        _killData.Collect();
    }

    private void OnEnemyHealthEnded(Enemy enemy)
    {
        _hero.AccrueLootReward(enemy.LootData);
    }

}
