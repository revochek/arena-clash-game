using TMPro;
using UnityEngine;

public class KillsCounter : MonoBehaviour
{
    [SerializeField] TMP_Text _killsText;
    [SerializeField] KillData _killData;

    private void OnEnable()
    {
        _killData.KillsChanged += OnKillsChanged;
    }

    private void OnDisable()
    {
        _killData.KillsChanged -= OnKillsChanged;
    }

    private void OnKillsChanged(int count)
    {
        _killsText.text = $"KILLS: {_killData.Count}";
    }
}