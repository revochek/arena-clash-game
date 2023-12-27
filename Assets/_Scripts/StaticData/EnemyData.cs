using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Game Data/Enemy Data")]
public abstract class EnemyData : ScriptableObject
{
    public int MaxHealth;
    public int StartHealth;
    public int PowerReward;
    public float MovementSpeed = 50f;
}