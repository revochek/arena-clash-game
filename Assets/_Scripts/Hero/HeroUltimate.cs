using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUltimate : MonoBehaviour
{
    [SerializeField] private HeroData _data;
    [SerializeField] private Hero _hero;

    private int _ultimatePowerPriced;

    private void Awake()
    {
        _ultimatePowerPriced = _data.MaxPower;
    }

    public void UseUltimate()
    {
        if (_hero.CheckSolvencyPower(_ultimatePowerPriced)) 
        {
            _hero.ToPayPower();
            EnemyManager.instance.DestroyAllEnemies(); 
        }
    }
}
