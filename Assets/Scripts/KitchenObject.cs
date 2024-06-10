
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSo;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newKitchenObjectParent)
    {
       
        if (newKitchenObjectParent.HasKitchenObject())
        {
            //Debug.LogError("Target ClearCounter already has an object");
            
           
            Destroy(gameObject);
            
            
            if (this.kitchenObjectParent != null)
            {
                this.kitchenObjectParent.ClearKitchenObject();
            }

            return; 
        }

        
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = newKitchenObjectParent;

        
        if (newKitchenObjectParent != null)
        {
            newKitchenObjectParent.SetKitchenObject(this);
            transform.parent = newKitchenObjectParent.GetkitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("Passed newKitchenObjectParent is null");
        }
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestorySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO newKitchenObjectSO, IKitchenObjectParent newIKichenObjectParent)
    {
        Transform KitchenObjectTransform = Instantiate(newKitchenObjectSO.prefab);
        KitchenObject kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(newIKichenObjectParent);
        return kitchenObject;
    }
}