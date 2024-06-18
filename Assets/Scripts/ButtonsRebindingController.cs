using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsRebindingController : MonoBehaviour
{
    [SerializeField] private Button MoveUp;
    [SerializeField] private Button MoveDown;
    [SerializeField] private Button MoveLeft;
    [SerializeField] private Button  MoveRight;
    [SerializeField] private Button  Interact;
    [SerializeField] private Button  InteractAlternate;
    [SerializeField] private Button  Pause;
    
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI  MoveRightText;
    [SerializeField] private TextMeshProUGUI  InteractText;
    [SerializeField] private TextMeshProUGUI  InteractAlternateText;
    [SerializeField] private TextMeshProUGUI  ESCText;

    [SerializeField] private Transform presstoRebindKeyTransform;


    private void OnEnable()
    {
        UpdateVisuals();
        HidePressRebindKey();
    }

    private void Update()
    {
        MoveUp.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.MoveUp);});
        MoveDown.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.MoveDown);});
        MoveLeft.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.MoveLeft);});
        MoveRight.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.MoveRight);});
        Interact.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Interact);});
        InteractAlternate.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.InteractAlternate);});
        Pause.onClick.AddListener(() =>{RebindBinding(GameInput.Bindings.Pause);});
    }

    private void UpdateVisuals()
    {
        MoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        MoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        MoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        MoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Interact);
        InteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.InteractAlternate);
        ESCText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.Pause);

        
       


    }


    private void ShowPressRebindKey()
    {
        presstoRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressRebindKey()
    {
        presstoRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Bindings bindings)
    {
        ShowPressRebindKey();
        GameInput.Instance.RebinDBinding(bindings, () =>
        {
            HidePressRebindKey();
            UpdateVisuals();
        });
    }
    
}
