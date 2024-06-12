using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
   [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

   private float spawnPlateTimer;
   private const float spawnPlateTimerMax = 4f;
   private int platesSpawnedAmount=4;
   private const  int platesSpawnedAmountMax = 4;


   private void Start()
   {
       for (int i = 0; i < 4; i++)
       {
           OnPlateSpawned?.Invoke(this,EventArgs.Empty);
       }
   }

   private void Update()
   {
      spawnPlateTimer += Time.deltaTime;
      if (spawnPlateTimer > spawnPlateTimerMax)
      {
          spawnPlateTimer = 0f;

          if (platesSpawnedAmount < platesSpawnedAmountMax)
          {
              platesSpawnedAmount++;
              
              OnPlateSpawned?.Invoke(this,EventArgs.Empty);
          }
      }
   }
   public override void Interact(Move player)
   {
       if (!player.HasKitchenObject())
       {
           if(platesSpawnedAmount > 0)
           {
               platesSpawnedAmount--;
               KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
               
               OnPlateRemoved?.Invoke(this,EventArgs.Empty);
             
           }
       }
   }
   
}
