using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUi : MonoBehaviour
{
  [SerializeField] private Image barIamge;
  [SerializeField] private CuttinCounter _cuttinCounter;


  private void Start()
  {
    _cuttinCounter.OnProgressChanged += CuttingCounter_OnPorgressChanged;
    barIamge.fillAmount = 0f;
    Hide();
  }

  private void CuttingCounter_OnPorgressChanged(object sender, CuttinCounter.OnProgressChangedEventArgs e)
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
