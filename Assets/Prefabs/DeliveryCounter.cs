using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter

{
    public override void Interact(Move player)
    {
        if (player.TryGetPlateObject(out PlateKitchenObject plateKitchenObject))
        {
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
           plateKitchenObject.DestorySelf();
        }
    }
}



