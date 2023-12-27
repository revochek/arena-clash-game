using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup CanvasGroup;

    public abstract void Open();
    public abstract void Close();

    public void Activate()
    {
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }
    public void Deactivate()
    {
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }
}