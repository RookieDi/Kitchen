using UnityEngine;

[CreateAssetMenu()]
public class BurningReceipeSo : ScriptableObject
{
   public KitchenObjectSO input;
   public KitchenObjectSO output;
   public float burningTimerTimeMax;
}
