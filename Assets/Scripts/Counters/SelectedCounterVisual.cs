using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        if (Move.LocalInstance != null)
        {
            Move.LocalInstance.OnSelectedCounterchanged += Move_OnSelectedCounterChanged;
        }

        else
        {
            Move.OnAnyPlayerSpawned += OnAnyPlanyerSpawnedLocal;
        }
    }

    private void OnAnyPlanyerSpawnedLocal(object sender, EventArgs e)
    {
        if (Move.LocalInstance != null)
        {
            Move.LocalInstance.OnSelectedCounterchanged -= Move_OnSelectedCounterChanged;
            Move.LocalInstance.OnSelectedCounterchanged += Move_OnSelectedCounterChanged;
            
        }
    }

    private void Move_OnSelectedCounterChanged(object sender, Move.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
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
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
      
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
