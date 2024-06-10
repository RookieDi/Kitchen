using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSo;
    public override void  Interact(Move player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

            else
            {

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                
            }
            else
            {
                
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Move player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().DestorySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSo,this);
           
        }
    }
}
