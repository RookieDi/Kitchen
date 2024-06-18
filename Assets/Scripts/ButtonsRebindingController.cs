using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsRebindingController : MonoBehaviour
{
   /* [SerializeField] private Button MoveUp;
    [SerializeField] private Button MoveDown;
    [SerializeField] private Button MoveLeft;
    [SerializeField] private Button  MoveRight;
    [SerializeField] private Button  Interact;
    [SerializeField] private Button  InteractAlternate;
    [SerializeField] private Button  ESC;
    */
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI  MoveRightText;
   // [SerializeField] private TextMeshProUGUI  InteractText;
  //  [SerializeField] private TextMeshProUGUI  InteractAlternateText;
  //  [SerializeField] private TextMeshProUGUI  ESCText;


    private void OnEnable()
    {
        UpdateVisuals();
        Debug.Log(MoveUpText.text);
        Debug.Log(GameInput.Instance);
        Debug.Log( GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp));
        Debug.Log(MoveUpText);
    }


    private void UpdateVisuals()
    {
        MoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveUp);
        MoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveDown);
        MoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveLeft);
        MoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Bindings.MoveRight);
       


    }
    
    
    
    
    
}
