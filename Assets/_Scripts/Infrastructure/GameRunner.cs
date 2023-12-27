using UnityEngine;

public class GameRunner : MonoBehaviour
{
    [SerializeField] private LevelBuilder _levelBuilder;

    private void Awake()
    {
        _levelBuilder.OnLoaded();
    }
}