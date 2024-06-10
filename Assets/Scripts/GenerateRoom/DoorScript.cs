using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorObject; // Reference to the door object
    private Quaternion closedRotation; // The rotation when the door is closed
    private Quaternion openedRotation; // The rotation when the door is opened
    private bool isOpen = false; // Flag to track if the door is open

    void Start()
    {
        // Store the initial rotation as closed rotation
        closedRotation = doorObject.transform.rotation;

        // Calculate the opened rotation (e.g., rotate around the y-axis by 90 degrees)
        openedRotation = Quaternion.Euler(0, 90, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Move" script attached
        Move moveScript = other.GetComponent<Move>();
        if (moveScript != null)
        {
            OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the colliding object has the "Move" script attached
        Move moveScript = other.GetComponent<Move>();
        if (moveScript != null)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            
            doorObject.transform.rotation = openedRotation;
            isOpen = true;
        }
    }

    void CloseDoor()
    {
        if (isOpen)
        {
            // Rotate the door back to the closed position
            doorObject.transform.rotation = closedRotation;
            isOpen = false;
        }
    }
}