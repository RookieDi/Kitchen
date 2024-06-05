using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
   private PlayerInputActions _playerInputActions;
   public event EventHandler OnInteractAction;
   
   private void Awake()
   {
     _playerInputActions = new PlayerInputActions();
    _playerInputActions.Enable();

    _playerInputActions.Player.Interact.performed += Interact_performed;
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
