using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUi : MonoBehaviour
{
  [SerializeField] private Image barIamge;
  [SerializeField] private GameObject hasProgressGameObject;
  private IHasProgressBar hasProgress;


  private void Start()
  {

    hasProgress = hasProgressGameObject.GetComponent<IHasProgressBar>();
   
    hasProgress.OnProgressChanged += HasProgress_OnPorgressChanged;
    barIamge.fillAmount = 0f;
    Hide();
  }

  private void HasProgress_OnPorgressChanged(object sender, IHasProgressBar.OnProgressChangedEventArgs e)
  {
    barIamge.fillAmount = e.progreesNormalized;
    if (e.progreesNormalized == 0f || e.progreesNormalized ==1f)
    {
      Hide();
    }
    else
    {
      Show();
    }
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }
}
