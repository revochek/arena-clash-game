using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Game Data/Hero Character Data")]
public class HeroData : ScriptableObject
{
    public int MaxHealth = 100;
    public int MaxPower = 100;
    public int StartHealth = 100;
    public int StartPower = 50;
    public int CriticalHealth = 10;
    public float MovementSpeed = 150f;
}