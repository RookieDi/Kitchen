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
            if ( player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

            else
            {
                //Player not carrying
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                
            }
            else
            {
                //Player not carrying anithing
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
