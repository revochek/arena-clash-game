using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyFactory : IEnemyFactory
{
    [SerializeField] private Enemy _redEnemyTemplate;

    public Enemy CreateEnemy(Vector3 spawnPoint, Transform parent)
    {
        return GameObject.Instantiate(Resources.Load<Enemy>(AssetPath.BlueEnemyPath), spawnPoint, Quaternion.identity, parent);
    }
}
