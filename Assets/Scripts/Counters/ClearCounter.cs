using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    
    public override void  Interact(Move player)
    {
        if (!HasKitchenObject())
        {
            if ( player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

           
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                 
                  if ( plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                  {
                      GetKitchenObject().DestorySelf();
                      
                  }
                }
                else
                {
                    //Player is not Carryinh plate but smth else
                    if (GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        //Counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestorySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

 
}