using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCountDownTimerUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI countdownTimerText;
   private const float countDownToStart = 3f;

   private void Start()
   {
      GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
      Hide();
   }

   private void GameManager_OnStateChanged(object sender, EventArgs e)
   {
       if (GameManager.Instance.IsCountDownToStartAtcitv())
       {
           Show();
       }
       else
       {
           Hide();
       }
   }

   private void Hide()
   {
       countdownTimerText.gameObject.SetActive(false); 
       StopCoroutine(startCounting());
       
   }

   private void Show()
   {
       countdownTimerText.gameObject.SetActive(true);
       StartCoroutine(startCounting());
      
   }

   IEnumerator startCounting()
   {
       float maxTime = countDownToStart;



       do
       {
           maxTime -= 1f;
           yield return new WaitForSeconds(1f);
           countdownTimerText.text = maxTime.ToString();
           
       } while (maxTime >= 0);



   }
}
