using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _playerRadius = 0.1f;
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask countersLayerMask;
  
    private bool _isWalking;
    private Vector3 lastInteractDir;

    private void Update()
    {
       
       HandleMovement();
       HandleInteraction();
        
    }
    
    public bool IsWalking()
    {
        return !_isWalking;
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0f, inputVector.y);
        float interactDistance = 1f;
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
        else
        {
            Debug.Log("sal");
        }

    }

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove= !Physics.CapsuleCast(transform.position,transform.position +Vector3.up*_playerHeight,_playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up*_playerHeight,_playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up*_playerHeight,_playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //  
                }
            }
        }
        
        
        
        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        _isWalking = moveDir != Vector3.zero;
            
            

           
        float rotateSpeed = 5f; 
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);


    }
}
