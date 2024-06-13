using System;
using UnityEngine;


public class Move : MonoBehaviour,IKitchenObjectParent
{
    public static Move Instance { get; private set; }

    
    public event EventHandler <OnSelectedCounterChangedEventArgs>OnSelectedCounterchanged;
    
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private float _playerRadius = 0.1f;
    [SerializeField] private float _playerHeight = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask countersLayerMask;
   [SerializeField] private Transform kitchenObjectHoldPoint;
    private BaseCounter selectedCounter;
  
    private bool _isWalking;
    private Vector3 lastInteractDir;

    private KitchenObject kitchenObj;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is more than one player instance!!!");
        }
    }

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteraction;
        _gameInput.OnInteractAlteranteAction += GameInput_OnAlternateInteraction;
    }

    private void GameInput_OnAlternateInteraction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
           
        }
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
       if(selectedCounter != null)
       {
           selectedCounter.Interact(this);
           
       }
    }

    private void Update()
    {
       
       HandleMovement();
      HandleInteraction();
        
    }
    
    public bool IsWalking()
    {
        return _isWalking;
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                
                
                SetSelectedCounter(baseCounter);
                
                
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
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
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position,transform.position +Vector3.up*_playerHeight,_playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove =moveDir.z !=0 && !Physics.CapsuleCast(transform.position,transform.position +Vector3.up*_playerHeight,_playerRadius, moveDirZ, moveDistance);
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
            
            

           
        float rotateSpeed = 10f; 
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);


    }

    private void SetSelectedCounter(BaseCounter newselectedCounter)
    {
        selectedCounter = newselectedCounter;
        OnSelectedCounterchanged?.Invoke(this,new OnSelectedCounterChangedEventArgs()
            {
        selectedCounter = newselectedCounter
        });
    }

    public Transform GetkitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }


    public bool TryGetPlateObject(out PlateKitchenObject plateKitchenObject)
    {
        if (kitchenObj.TryGetComponent(out PlateKitchenObject plate))
        {
            plateKitchenObject = plate;
            return true;
        }

        plateKitchenObject = null;
        return false;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObj = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObj;
    }

    public void ClearKitchenObject()
    {
        kitchenObj = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObj != null;
    }
    
    
    
}
