using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CuttingCounterVisual : MonoBehaviour
{
   private Animator _animator;

   [SerializeField] private CuttinCounter cuttinCounter;

   private const string CUT = "Cut";

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Start()
   {
      cuttinCounter.OnCut += CuttinCounter_OnCut;
     
   }

   private void CuttinCounter_OnCut(object sender,System.EventArgs e)
   {
     _animator.SetTrigger(CUT);
   }
}
