using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    
    public enum Bindings
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause
    }
    
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

   public string GetBindingText(Bindings bindings)
   {
       switch (bindings)
       {
           case Bindings.MoveUp:
               return _playerInputActions.Player.Move.bindings[1].ToDisplayString();
           case Bindings.MoveDown:
               return _playerInputActions.Player.Move.bindings[2].ToDisplayString();
           case Bindings.MoveLeft:
               return _playerInputActions.Player.Move.bindings[3].ToDisplayString();
           case Bindings.MoveRight:
               return _playerInputActions.Player.Move.bindings[4].ToDisplayString();
           case Bindings.Interact:
               return _playerInputActions.Player.Interact.bindings[0].ToDisplayString();
           case Bindings.InteractAlternate:
              return _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
           case Bindings.Pause:
               return _playerInputActions.Player.Pause.bindings[0].ToDisplayString();
           default:
               return "Key Not Found";
           
       }
   }

   public void RebinDBinding(Bindings binding,Action onActionRebound)
   {
       InputAction inputAction;
       int bindingIndex;
       switch (binding)
       {
           default: 
           case Bindings.MoveUp:
               inputAction = _playerInputActions.Player.Move;
               bindingIndex = 1;
           break;
           case Bindings.MoveDown:
               inputAction = _playerInputActions.Player.Move;
               bindingIndex = 2;
               break;
           case Bindings.MoveRight:
               inputAction = _playerInputActions.Player.Move;
               bindingIndex = 4;
               break;
           case Bindings.MoveLeft:
               inputAction = _playerInputActions.Player.Move;
               bindingIndex = 3;
               break;
           case Bindings.Interact:
               inputAction = _playerInputActions.Player.Interact;
               bindingIndex = 5;
               break;
           case Bindings.InteractAlternate:
               inputAction = _playerInputActions.Player.InteractAlternate;
               bindingIndex = 6;
               break;
           case Bindings.Pause:
               inputAction = _playerInputActions.Player.Pause;
               bindingIndex = 7;
               break;
       }
       _playerInputActions.Player.Disable();

      inputAction.PerformInteractiveRebinding(bindingIndex)
           .OnComplete(callback =>
           {
               callback.Dispose();
               _playerInputActions.Player.Enable();
               onActionRebound();
           })
           .Start();
   }
  
}
