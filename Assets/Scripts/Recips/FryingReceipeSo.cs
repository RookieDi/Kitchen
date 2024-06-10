using UnityEngine;

[CreateAssetMenu()]
public class FryingReceipeSo : ScriptableObject
{
   public KitchenObjectSO input;
   public KitchenObjectSO output;
   public float fryingTimeMax;
}
