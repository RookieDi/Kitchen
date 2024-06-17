using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    
    public static GameInput Instance { get; private set; }
   private PlayerInputActions _playerInputActions;
   public event EventHandler OnInteractAction;
   public event EventHandler OnInteractAlteranteAction;
   public event EventHandler OnpauseAction;
   
   
   private void Awake()
   {
       Instance = this;
     _playerInputActions = new PlayerInputActions();
    _playerInputActions.Enable();

    _playerInputActions.Player.Interact.performed += Interact_performed;
    _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_Performed;
    _playerInputActions.Player.Pause.performed += Pause_performed;
   }

   private void OnDestroy()
   {
       _playerInputActions.Player.Interact.performed -= Interact_performed;
       _playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_Performed;
       _playerInputActions.Player.Pause.performed -= Pause_performed; 
       
       
       _playerInputActions.Dispose();
   }

   private void Pause_performed(InputAction.CallbackContext obj)
   {
       OnpauseAction?.Invoke(this,EventArgs.Empty);
   }

   private void InteractAlternate_Performed(InputAction.CallbackContext obj)
   {
       if (OnInteractAlteranteAction != null)
       {
           OnInteractAlteranteAction?.Invoke(this, EventArgs.Empty);
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
