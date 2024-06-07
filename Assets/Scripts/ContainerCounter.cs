using System;
using UnityEngine;


public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;



    public override void Interact(Move player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }

    }
}
