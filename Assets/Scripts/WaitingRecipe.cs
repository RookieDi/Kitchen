using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRecipe
{
    public RecipeSo Recipe { get; private set; }
    public float RemainingTime { get; set; }
    public bool IsExpired { get; set; }

    public WaitingRecipe(RecipeSo recipe, float remainingTime)
    {
        Recipe = recipe;
        RemainingTime = remainingTime;
        IsExpired = false;
    }

}
