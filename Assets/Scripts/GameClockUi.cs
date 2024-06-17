using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUi : MonoBehaviour
{
   [SerializeField] private Image timeImage;

   private void Update()
   {
      timeImage.fillAmount=GameManager.Instance.GetGamePlayingTimerNormalized();
   }
}
