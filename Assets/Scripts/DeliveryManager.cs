using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : NetworkBehaviour
{

    public event EventHandler OnRecipeAdded,OnRecipeCompleted;
    public event EventHandler OnRecipeSucces, OnRecipeFailed;
    
    
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeList _recipeList;
    private List<RecipeSo> waitingRecipeSOList=new List<RecipeSo>();


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 8f;
    private int waitingRecipesMax = 7;
    private float spawnDelay = 2f; // Delay between recipe spawns
    private int Succesful;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Debug.Log("First Frame");
        if (!IsServer)
        {
            return;
        }
        Debug.Log("2 Frame");
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                StartCoroutine(SpawnRecipesWithDelay());
            }
        }

        if (waitingRecipeSOList.Count < waitingRecipesMax)
        {
            int waitingRecipeSoIndex = Random.Range(0, _recipeList.recipeSOList.Count);
            SpawnedNewWaitingRecipeClientRpc(waitingRecipeSoIndex);
          
        }
    }

    [ClientRpc]
    private void SpawnedNewWaitingRecipeClientRpc(int waitingRecipeSoIndex)
    {
        RecipeSo waitingRecipeSo = _recipeList.recipeSOList[waitingRecipeSoIndex];
        waitingRecipeSOList.Add(waitingRecipeSo);
           
        OnRecipeAdded?.Invoke(this,EventArgs.Empty);
    }
    
    private IEnumerator SpawnRecipesWithDelay()
    {
        while (waitingRecipeSOList.Count < waitingRecipesMax)
        {
            RecipeSo waitingRecipeSo = _recipeList.recipeSOList[Random.Range(0, _recipeList.recipeSOList.Count)];
            Debug.Log(waitingRecipeSo.recipeName);
            waitingRecipeSOList.Add(waitingRecipeSo);
            OnRecipeAdded?.Invoke(this, EventArgs.Empty);

            yield return new WaitForSeconds(spawnDelay); // Wait for the delay before spawning the next recipe
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
                    Succesful++;
                    Debug.Log("Player deliver the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    OnRecipeSucces?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }
        }
        //No matches
        //Incorect recipe
       OnRecipeFailed?.Invoke(this,EventArgs.Empty);
    }

    public List<RecipeSo> GetWaitingRecipesList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccesfullDeliveries()
    {
        return Succesful;
    }
}
