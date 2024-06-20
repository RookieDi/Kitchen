using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUi : MonoBehaviour
{
    public static DeliveryManagerUi Instance { get; private set; }

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        Instance = this;
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
       
        DeliveryManager.Instance.OnRecipeAdded += UpdateVisual;
        DeliveryManager.Instance.OnRecipeCompleted += UpdateVisual;
        DeliveryManager.Instance.OnRecipeSuccess += UpdateVisual;
        DeliveryManager.Instance.OnRecipeFailed += HandleRecipeFailed;

     
        UpdateVisual(this, EventArgs.Empty);
    }

    public void UpdateVisual(object sender, EventArgs args)
    {
       
        foreach (Transform child in container)
        {
            if (child != recipeTemplate)
            {
                Destroy(child.gameObject);
            }
        }

       
        foreach (var waitingOrder in DeliveryManager.Instance.GetWaitingRecipesList())
        {
            Transform instantiatedTemplate = Instantiate(recipeTemplate, container);
            instantiatedTemplate.gameObject.SetActive(true);
            var deliveryManagerSingleUi = instantiatedTemplate.GetComponent<DeliverManagerSingleUi>();
            deliveryManagerSingleUi.SetUpUi(waitingOrder.Recipe);
        }
    }

    public void UpdateTimer(RecipeSo recipe, float remainingTime)
    {
        foreach (Transform child in container)
        {
            DeliverManagerSingleUi uiElement = child.GetComponent<DeliverManagerSingleUi>();
            if (uiElement != null && uiElement.RecipeSo == recipe)
            {
                uiElement.UpdateTimerText(remainingTime.ToString("F1"));

              
                if (remainingTime <= 0.0f)
                {
                    ShowExpiredRecipe(recipe); 
                }

                break;
            }
        }
    }

    public void ShowExpiredRecipe(RecipeSo recipe)
    {
        foreach (Transform child in container)
        {
            DeliverManagerSingleUi uiElement = child.GetComponent<DeliverManagerSingleUi>();
            if (uiElement != null && uiElement.RecipeSo == recipe)
            {
                
                uiElement.ShowXIcon();

                
                StartCoroutine(DelayedDestroy(uiElement.gameObject));
                break;
            }
        }
    }

    private IEnumerator DelayedDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(1f); 

      
        Destroy(obj);
    }

    private void HandleRecipeFailed(object sender, EventArgs args)
    {
        Debug.Log("Recipe delivery failed!");
    }
}
