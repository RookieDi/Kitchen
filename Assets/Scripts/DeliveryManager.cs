using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeAdded;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeList _recipeList;
    private List<WaitingRecipe> waitingRecipeList = new List<WaitingRecipe>();

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 8f;
    private int waitingRecipesMax = 7;
    private float spawnDelay = 5f;
    private int successfulDeliveries;
    private float recipeLifetime = 5f;
    private bool isFirstBatchSpawned = false;

    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip recipeExpiredSound;
    [SerializeField] private AudioClip recipeFailedSound;

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
            if (waitingRecipeList.Count < waitingRecipesMax)
            {
                StartCoroutine(SpawnRecipesWithDelay());
            }
        }

        for (int i = waitingRecipeList.Count - 1; i >= 0; i--)
        {
            WaitingRecipe waitingRecipe = waitingRecipeList[i];
            waitingRecipe.RemainingTime -= Time.deltaTime;

            DeliveryManagerUi.Instance.UpdateTimer(waitingRecipe.Recipe, waitingRecipe.RemainingTime);

            if (waitingRecipe.RemainingTime <= 0 && !waitingRecipe.IsExpired)
            {
                waitingRecipe.IsExpired = true;
                OnRecipeExpired(waitingRecipe);
                break; 
            }
        }
    }

    private void OnRecipeExpired(WaitingRecipe expiredRecipe)
    {
        DeliveryManagerUi.Instance.ShowExpiredRecipe(expiredRecipe.Recipe);
        PlayRecipeExpiredSound(); 
        StartCoroutine(RemoveRecipeAfterDelay(expiredRecipe));
    }

    private IEnumerator RemoveRecipeAfterDelay(WaitingRecipe expiredRecipe)
    {
        yield return new WaitForSeconds(2f);

        waitingRecipeList.Remove(expiredRecipe);

        InvokeRecipeFailed();
    }

    private void InvokeRecipeFailed()
    {
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        PlayRecipeFailedSound();
    }

    private IEnumerator SpawnRecipesWithDelay()
    {
        while (waitingRecipeList.Count < waitingRecipesMax)
        {
            if (!isFirstBatchSpawned)
            {
                isFirstBatchSpawned = true;
                yield return new WaitForSeconds(4.5f); 
            }

            RecipeSo recipeSo = _recipeList.recipeSOList[Random.Range(0, _recipeList.recipeSOList.Count)];
            Debug.Log(recipeSo.recipeName);
            waitingRecipeList.Add(new WaitingRecipe(recipeSo, recipeLifetime));
            OnRecipeAdded?.Invoke(this, EventArgs.Empty);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSo recipeSo = waitingRecipeList[i].Recipe;

            if (recipeSo.KitchenObjectSoList.Count == plateKitchenObject.GetIngredientSo().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSo in recipeSo.KitchenObjectSoList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSo in plateKitchenObject.GetIngredientSo())
                    {
                        if (plateKitchenObjectSo == recipeKitchenObjectSo)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    successfulDeliveries++;
                    Debug.Log("Player delivered the correct recipe");
                    waitingRecipeList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        InvokeRecipeFailed();
    }

    public List<WaitingRecipe> GetWaitingRecipesList()
    {
        return waitingRecipeList;
    }

    public int GetSuccessfulDeliveries()
    {
        return successfulDeliveries;
    }

    private void PlayRecipeExpiredSound()
    {
        if (audioSource != null && recipeExpiredSound != null)
        {
            audioSource.PlayOneShot(recipeExpiredSound);
        }
    }

    private void PlayRecipeFailedSound()
    {
        if (audioSource != null && recipeFailedSound != null)
        {
            audioSource.PlayOneShot(recipeFailedSound);
        }
    }

   
}
