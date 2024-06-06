using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;



    public override void Interact(Move player)
    {
        if (!HasKitchenObject())
        {
            Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }

    }
}
