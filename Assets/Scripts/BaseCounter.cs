using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
 
 [SerializeField] private Transform counterTopPoint;
   

 public KitchenObject kitchenObj;

 public virtual void Interact(Move player)
 {
  Debug.LogError("BaseCounter.Interact();");
 }
 public virtual void InteractAlternate(Move player)
 {
  Debug.LogError("BaseCounter.InteractAlternate();");
 }
 public Transform GetkitchenObjectFollowTransform()
 {
  return counterTopPoint;
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
