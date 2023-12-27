using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyFactory : IEnemyFactory
{
    [SerializeField] private Enemy _redEnemyTemplate;

    public Enemy CreateEnemy(Vector3 spawnPoint, Transform parent)
    {
        return GameObject.Instantiate(Resources.Load<Enemy>(AssetPath.RedEnemyPath), spawnPoint, Quaternion.identity, parent);
    }
}
