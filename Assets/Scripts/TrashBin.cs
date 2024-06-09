using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : BaseCounter
{
   public  override void Interact(Move player)
   {
      if (player.HasKitchenObject())
      {
         player.GetKitchenObject().DestorySelf();
      }
   }
}
