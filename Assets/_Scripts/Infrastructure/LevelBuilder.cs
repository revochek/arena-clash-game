using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelBuilder
{
    public Hero Hero;
    public EnemySpawner EnemySpawner;
    public Game Game;
    public Camera Camera;

    public void OnLoaded()
    {
        InitGameWorld();
    }

    private void InitGameWorld()
    {
        InitHero();
        InitGame(Hero.GetComponent<HeroHealth>());
        InitEnemySpawner(Hero);
        CameraFollow(Hero.transform);
    }

    private void InitHero()
    {
        Hero.Initialize();
    }

    private void InitGame(HeroHealth heroHealth)
    {
        Game.Initialization(heroHealth);
    }

    private void InitEnemySpawner(Hero hero)
    {
        EnemySpawner.Initialization(hero);
    }

    private void CameraFollow(Transform target)
    {
        Camera.GetComponent<CameraFollow>().Follow(target);
    }
}
