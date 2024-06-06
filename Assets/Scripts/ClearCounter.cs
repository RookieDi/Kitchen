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

 
}