using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorScriptFunctional : MonoBehaviour
{
    [SerializeField] private float openSpeed = 3f; 
    private bool isOpening = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpening)
        {
            isOpening = true;
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        
        Vector3 targetPosition = transform.position + Vector3.up * 2f;

      
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; 
        rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, openSpeed * Time.deltaTime));
    }

}
