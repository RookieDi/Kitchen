using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverManagerSingleUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject xIcon;
    [SerializeField] private AudioSource audioSource;

    private RecipeSo recipeSo;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
        xIcon.SetActive(false);
    }

    public RecipeSo RecipeSo
    {
        get { return recipeSo; }
    }

    public void SetUpUi(RecipeSo recipeSo)
    {
        this.recipeSo = recipeSo;
        recipeNameText.text = recipeSo.name;

        foreach (Transform child in iconContainer)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var kitchenObjectSo in recipeSo.KitchenObjectSoList)
        {
            var instantiateIcon = Instantiate(iconTemplate, iconContainer);
            instantiateIcon.gameObject.SetActive(true);
            var image = instantiateIcon.transform.GetComponent<Image>();
            image.sprite = kitchenObjectSo.sprite;
        }

        
        UpdateTimerText("0.0"); 
    }


    public void UpdateTimerText(string time)
    {
        timerText.text = time;
    }

    public void ShowXIcon()
    {
        xIcon.SetActive(true);
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}