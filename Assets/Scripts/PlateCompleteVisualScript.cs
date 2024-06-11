using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteVisualScript : MonoBehaviour
{
    
    [Serializable]
    public struct KitchenObjectSo_GameObject
    {
        public KitchenObjectSO KitchenObjectSo;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSo_GameObject> _kitchenObjectSoGameObjectsList;

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngiridientAdded;
        foreach (KitchenObjectSo_GameObject kitchenObjectSoGameObject in _kitchenObjectSoGameObjectsList)
        {
            kitchenObjectSoGameObject.gameObject.SetActive(false);
        }

    }

    private void PlateKitchenObject_OnIngiridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        foreach (KitchenObjectSo_GameObject kitchenObjectSoGameObject in _kitchenObjectSoGameObjectsList)
        {
            if (kitchenObjectSoGameObject.KitchenObjectSo == e.kitchenObjectSo)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
       
    }
}
