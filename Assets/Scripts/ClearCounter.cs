using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObj;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObj != null)
            {
                kitchenObj.SetKitchenObjectParent(secondClearCounter);
            }
        }
    }

    public void Interact(Move player)
    {
        if (kitchenObj == null)
        {
            Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

        }
        else
        {
            kitchenObj.SetKitchenObjectParent(player);
        }
    }

    public Transform GetkitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObj = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObj;
    }

    public void ClearKitchenObject()
    {
        kitchenObj = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObj != null;
    }
}