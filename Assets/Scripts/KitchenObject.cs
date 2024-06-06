using System.Collections;
using System.Collections.Generic;
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





    public static KitchenObject SpawnKitchenObject(KitchenObjectSO newKitchenObjectSO,IKitchenObjectParent newIKitchenObjectParent)
    {
        Transform KitchenObjectTransform = Instantiate(newKitchenObjectSO.prefab);
        KitchenObject kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(newIKitchenObjectParent);
        return kitchenObject;
    }
}