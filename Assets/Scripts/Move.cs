using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   [SerializeField] private float speed = 3f; 

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
    }
}