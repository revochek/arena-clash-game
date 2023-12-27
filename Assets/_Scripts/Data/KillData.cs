using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "KillData", menuName = "Stats Data/Kill Data")]
public class KillData : ScriptableObject
{
    public int Count;
    public event UnityAction<int> KillsChanged;

    public void Collect()
    {
        Count++;
        KillsChanged?.Invoke(Count);
    }

    public void Clear()
    {
        Count = 0;
    }
}
