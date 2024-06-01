using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private const string IS_WALKING ="IsWalking";
    [SerializeField] private Move _player;

    private void Awake()
    {
         _animator = GetComponent<Animator>();
         _animator.SetBool(IS_WALKING,_player.IsWalking());
    }

    private void Update()
    {
        _animator.SetBool(IS_WALKING,_player.IsWalking());
    }
}