using System;
using UnityEngine;


public class CuttinCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progreesNormalized;
    }

    public event EventHandler OnCut;
    
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;

    public override void Interact(Move player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
                    {
                        progreesNormalized =  (float)cuttingProgress / cuttingRecipeSO.cuttingProgresMax
                    });
                }
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this,EventArgs.Empty);
            
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            
            
            OnProgressChanged?.Invoke(this,new OnProgressChangedEventArgs
            {
                progreesNormalized =  (float)cuttingProgress / cuttingRecipeSO.cuttingProgresMax
            });
            
            
            
            
            
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgresMax)
            {
            
                KitchenObjectSO outputKitchenObjcetSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestorySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjcetSo, this);
            }
            
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
            return cuttingRecipeSO.output;
        else
        {
            return null;
        }
      
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSo;
            }
        }

        return null;
    }
}
