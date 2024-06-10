using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSos;
    private List<KitchenObjectSO> _kitchenObjectSosList;

    private void Awake()
    {
        _kitchenObjectSosList = new List<KitchenObjectSO>();
    }

    public  bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {

        if (!validKitchenObjectSos.Contains(kitchenObjectSo))
        {
            return false;
        }
        if (_kitchenObjectSosList.Contains(kitchenObjectSo))
        {
            return false;
        }
        else
        {
            

            _kitchenObjectSosList.Add(kitchenObjectSo);
            return true;
        }
    }
}
