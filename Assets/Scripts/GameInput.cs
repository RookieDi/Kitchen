using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
   private PlayerInputActions _playerInputActions;
   public event EventHandler OnInteractAction;
   public event EventHandler OnInteractAlternateAction;
   
   private void Awake()
   {
     _playerInputActions = new PlayerInputActions();
    _playerInputActions.Enable();

    _playerInputActions.Player.Interact.performed += Interact_performed;
    _playerInputActions.Player.InteractAlternate.performed += InteractAlterante_perform;
   }

   private void InteractAlterante_perform(InputAction.CallbackContext obj)
   {
       if (OnInteractAlternateAction !=null)
       {
           OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
       }
       
   }

   private void Interact_performed(InputAction.CallbackContext obj)
   {
      
       //Debug.Log("dd");
       if (OnInteractAction != null)
       {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
       }
   }

   public Vector2 GetMovementVectorNormalized()
   {
       Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();
      
       
       return inputVector.normalized;

   }
  
}
