using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
   private Move player;
   private float footstepTimer;
   private float footstepTimerMax = .1f;
   
   private void Awake()
   {
      player = GetComponent<Move>();
   }

   private void Update()
   {
      footstepTimer -= Time.deltaTime;
      if (footstepTimer < 0f)
      {
         footstepTimer = footstepTimerMax;

         if (player.IsWalking())
         {
            SoundManager.Instance.PlayFootstepsSound(player.transform.position, 1f);
         }
      }
   }
}
