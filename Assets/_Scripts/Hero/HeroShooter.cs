using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroHealth))]
public class HeroShooter : MonoBehaviour
{
    [SerializeField] private HeroProjectile _projectile;
    [SerializeField] private HeroHealth _health;
    [SerializeField] private float _powerRewardChance;
    [SerializeField] private LayerMask _layerIgnoreMask;

    public void Shoot()
    {
        ShootProjectile();
    }

    private void ShootProjectile()
    {
        Vector3 target = EnemyManager.instance.FindClosestEnemyPosition(transform.position);
        if (target != Vector3.zero)
        {
            HeroProjectile projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
            projectile.Initialization(_health);
            projectile.RicochetKilled += OnRicochetKilled;
        }
    }

    private void OnRicochetKilled(HeroProjectile projectile)
    {
        AccrueLootRewardForRicochetKill(projectile.RicochetLootData);
    }
    private void AccrueLootRewardForRicochetKill(LootData lootRicochetKill)
    {
        if (Random.value >= _powerRewardChance)
        {
            _health.AccrueLootReward(lootRicochetKill);
        }
        else
        {
            _health.IncreaseHealthByHalf();
        }
    }

}
