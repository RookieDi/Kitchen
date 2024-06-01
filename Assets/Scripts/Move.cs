using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   private float speed = 3f;
   private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.y = +1; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.y = -1; 
        }
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.x = -1; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.x = +1;
        }
        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.y, 0f, inputVector.x);
        
        transform.position += moveDir * speed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;
        float rotation = 15f;
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotation);
        }
    }

    public bool IsWalking()
    {
        return !isWalking;
    }
}