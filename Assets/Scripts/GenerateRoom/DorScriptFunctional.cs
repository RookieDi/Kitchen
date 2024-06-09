using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorScriptFunctional : MonoBehaviour
{
    private Animator _doorAnimator;

    private void Start()
    {
        _doorAnimator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))

        {
            _doorAnimator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _doorAnimator.SetTrigger("Closed");
        }
    }
}
