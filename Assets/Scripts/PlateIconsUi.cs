using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUi : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform iconTemplatePrefab;
    [SerializeField] private Image ingredientIcon;
    private GameObject[] SpawnedTemplatesArray;
    private List<GameObject> SpawnedTemplatesList = new List<GameObject>();

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += OnPlateIngridientAdded;
    }

    private void OnPlateIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        int index = 0;
        
        //Debug.Log(SpawnedTemplatesList.Length);
        SpawnedTemplatesArray = new GameObject[SpawnedTemplatesList.Count];
        for (int i = 0; i < SpawnedTemplatesArray.Length; i++)
        {
           SpawnedTemplatesArray[i] = SpawnedTemplatesList.ElementAt(i);
           
        }
        for (int i = 0; i < SpawnedTemplatesArray.Length; i++)
        {
            
            Destroy(SpawnedTemplatesArray[i]);
        }
        

        SpawnedTemplatesList.Clear();
        foreach (KitchenObjectSO kitchenObjectSo in _plateKitchenObject.GetIngredientSo())
        {
           
            ingredientIcon.sprite = kitchenObjectSo.sprite;
            var instantiated = Instantiate(iconTemplatePrefab, transform);
            SpawnedTemplatesList.Add(instantiated.gameObject);
            
        }
        
    }
}