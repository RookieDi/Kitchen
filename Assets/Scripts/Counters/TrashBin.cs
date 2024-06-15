using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : BaseCounter
{
   public static event EventHandler OnAnyObjTrashed;
   public  override void Interact(Move player)
   {
      if (player.HasKitchenObject())
      {
         player.GetKitchenObject().DestorySelf();
         OnAnyObjTrashed?.Invoke(this,EventArgs.Empty);
      }
   }
}
