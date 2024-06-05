using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject selectedCounterVisual;
    [SerializeField] private ClearCounter _clearCounter;
    private void Start()
    {
        Move.Instance.OnSelectedCounterchanged += Move_OnSelectedCounterChanged;
    }

    private void Move_OnSelectedCounterChanged(object sender, Move.OnSelectedCounterChangedEventArgs e)
    {
        if (_clearCounter == e.selectedCounter)
        {
         Show();   
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        selectedCounterVisual.SetActive(true);   
    }
    private void Hide()
    {
        selectedCounterVisual.SetActive(false);   
    }
}
