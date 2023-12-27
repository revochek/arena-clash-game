using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnCircleArea : SpawnLocation
{
    [SerializeField] private float _minSpawnRadius;
    [SerializeField] private float _maxSpawnRadius;
    private void OnValidate()
    {
        if (_minSpawnRadius >= _maxSpawnRadius) 
            _maxSpawnRadius = _minSpawnRadius;

        if (_maxSpawnRadius <= _minSpawnRadius) 
            _minSpawnRadius = _maxSpawnRadius;
    }

    public override Transform GetParentTransform()
    {
        return transform;
    }

    public override Vector3 GetSpawnPoint()
    {
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * Random.Range(_minSpawnRadius, _maxSpawnRadius);
        return transform.position + new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y);
    }
}
