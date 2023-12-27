using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;
    [SerializeField] private TMP_Text _scoreText;

    public void OnValueChanged(int value, int maxValue)
    {
        _scoreText.text = value.ToString();
        Slider.value = (float)value / maxValue;
    }
}
