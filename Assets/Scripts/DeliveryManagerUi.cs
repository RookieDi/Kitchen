using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUi : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeAdded += OnWaitingRecipesListChanged;
        DeliveryManager.Instance.OnRecipeCompleted += OnWaitingRecipesListChanged;
    }

    private void OnWaitingRecipesListChanged(object sender, EventArgs e)
    {
     UpdateVisual();
    }

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    public void UpdateVisual()
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
            Transform instantiatedTemplate= Instantiate(recipeTemplate, container);
               
            instantiatedTemplate.gameObject.SetActive(true);
           var deliveryManagerSingleUi= instantiatedTemplate.GetComponent<DeliverManagerSingleUi>();
           deliveryManagerSingleUi.SetUpUi(waitingOrder);
        }
    }
}
