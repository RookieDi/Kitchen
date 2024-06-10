using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgressBar
{
   public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
   public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;

   public class OnStateChangedEventArgs : EventArgs
   {
      public State state;
   }
   
   public enum State
   {
      Idle,
      Frying,
      Fried,
      Burned,
   }
   [SerializeField] private FryingReceipeSo[] _fryingReceipeSosArray;
   [SerializeField] private BurningReceipeSo[] _burningReceipeSosArray;

   private float fryingTimer;
   private float burningTimer;
   private BurningReceipeSo burningReceipeSo;
   private FryingReceipeSo fryingReceipeSo;

   private State state;

   private void Start()
   {
      state = State.Idle;
   }

   private void Update()
   {
      if (HasKitchenObject())
      {

         switch (state)
         {
            case State.Idle:
               break;
            case State.Frying:
               fryingTimer += Time.deltaTime;
               
               OnProgressChanged?.Invoke(this,new IHasProgressBar .OnProgressChangedEventArgs
               {
                  progreesNormalized = fryingTimer / fryingReceipeSo.fryingTimeMax
               });
               
               if (fryingTimer > fryingReceipeSo.fryingTimeMax)
               {
                  
                  GetKitchenObject().DestorySelf();

                  KitchenObject.SpawnKitchenObject(fryingReceipeSo.output, this);
                
                  state = State.Fried;
                  burningTimer = 0f;
                  burningReceipeSo = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                  OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                     state = state
                     });
               }
               break;
            case State.Fried:
               burningTimer += Time.deltaTime;
               OnProgressChanged?.Invoke(this,new IHasProgressBar .OnProgressChangedEventArgs
               {
                  progreesNormalized = burningTimer / burningReceipeSo.burningTimerTimeMax
               });
               if (burningTimer > burningReceipeSo.burningTimerTimeMax)
               {
                  
                  GetKitchenObject().DestorySelf();

                  KitchenObject.SpawnKitchenObject(burningReceipeSo.output, this);
                  
                  state = State.Burned;
                  OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                     state = state
                     
                  });
                  
                  OnProgressChanged?.Invoke(this,new IHasProgressBar .OnProgressChangedEventArgs
                  {
                     progreesNormalized =0f
                  });
               }
               break;
            case State.Burned:
               break;
         }
        
      }


   }

   public override void Interact(Move player)
   {
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
               player.GetKitchenObject().SetKitchenObjectParent(this);
               fryingReceipeSo = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
               state = State.Frying;
               fryingTimer = 0f;
               OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                  state = state
               });
               OnProgressChanged?.Invoke(this,new IHasProgressBar .OnProgressChangedEventArgs
               {
                  progreesNormalized = fryingTimer / fryingReceipeSo.fryingTimeMax
               });

            }
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
                  state = State.Idle;
                  OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                     state = state
                  });
                  OnProgressChanged?.Invoke(this,new IHasProgressBar .OnProgressChangedEventArgs
                  {
                     progreesNormalized =0f
                  });
                      
               }
            }
         }
         else
         {
            GetKitchenObject().SetKitchenObjectParent(player);
            state = State.Idle;
              OnStateChanged?.Invoke(this,new OnStateChangedEventArgs{
                                  state = state
                                  });
                                  OnProgressChanged?.Invoke(this,new IHasProgressBar .OnProgressChangedEventArgs
                                  {
                                     progreesNormalized =0f
                                  });
         }
      }
   }
   private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      FryingReceipeSo fryingReceipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
      return fryingReceipeSo != null;
   }

   private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
   {
      FryingReceipeSo fryingReceipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
      if (fryingReceipeSo != null)
         return fryingReceipeSo.output;
      else
      {
         return null;
      }
      
   }

   private FryingReceipeSo GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (FryingReceipeSo fryingReceipeSo in _fryingReceipeSosArray)
      {
         if (fryingReceipeSo.input == inputKitchenObjectSO)
         {
            return fryingReceipeSo;
         }
      }

      return null;
   }
   private BurningReceipeSo GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
   {
      foreach (BurningReceipeSo burningReceipeSo in _burningReceipeSosArray)
      {
         if (burningReceipeSo.input == inputKitchenObjectSO)
         {
            return burningReceipeSo;
         }
      }

      return null;
   }
}
