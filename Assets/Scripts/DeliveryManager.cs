using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeAdded,OnRecipeCompleted; 
    
    
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeList _recipeList;
    private List<RecipeSo> waitingRecipeSOList=new List<RecipeSo>();


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
        }

        if (waitingRecipeSOList.Count < waitingRecipesMax)
        {
            RecipeSo waitingRecipeSo = _recipeList.recipeSOList[Random.Range(0, _recipeList.recipeSOList.Count)];
           Debug.Log(waitingRecipeSo.recipeName);
           waitingRecipeSOList.Add(waitingRecipeSo);
           
           OnRecipeAdded?.Invoke(this,EventArgs.Empty);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSo waitingRecipeSo = waitingRecipeSOList[i];

            if (waitingRecipeSo.KitchenObjectSoList.Count == plateKitchenObject.GetIngredientSo().Count)
            {
                bool plateContentsMatchesRecipe = true;
                //Has the same number of ingridients
                foreach (KitchenObjectSO recipeKitchenObjectSo in waitingRecipeSo.KitchenObjectSoList)
                {
                    bool ingridientFound = false;
                    //Cycling through all ingridients in the recipe
                    foreach (KitchenObjectSO plateKitchenObjectSo in plateKitchenObject.GetIngredientSo())
                    {
                        //Through all ingridients in the plate

                        if (plateKitchenObjectSo == recipeKitchenObjectSo)
                        {
                            //ingridient match
                            ingridientFound = true;
                            break;
                        }
                    }

                    if (!ingridientFound)
                    {
                        //ingridient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //Player deliver the correct recipe
                    
                    Debug.Log("Player deliver the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }
        }
        //No matches
        //Incorect recipe
        Debug.Log("Incorect recipe");
    }

    public List<RecipeSo> GetWaitingRecipesList()
    {
        return waitingRecipeSOList;
    }
}
