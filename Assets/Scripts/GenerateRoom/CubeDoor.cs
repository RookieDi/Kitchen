using UnityEngine;

public class CubeDoor : MonoBehaviour
{
    public Transform player; // Referința către transformarea jucătorului
    public float openDistance = 3f; // Distanța la care ușa se va deschide
    public float closeDistance = 4f; // Distanța la care ușa se va închide
    public float doorSpeed = 2f; // Viteza de deschidere și închidere a ușii

    public Transform door; // Referința către transformarea cubului ușii

    private Vector3 initialPosition; // Poziția inițială a cubului ușii
    private Vector3 raisedPosition; // Poziția ridicată a cubului ușii
    private bool isRaised = false; // Starea cubului ușii (ridicat/coborât)

    void Start()
    {
        initialPosition = door.position;
        raisedPosition = initialPosition + Vector3.up * 2f; // Ridicăm cubul cu 2 unități pe axa Y
    }

    void Update()
    {
        // Calculăm distanța dintre jucător și ușă
        float distanceToPlayer = Vector3.Distance(player.position, door.position);

        // Verificăm dacă ușa trebuie să fie deschisă sau închisă în funcție de distanța față de jucător
        if (distanceToPlayer < openDistance && !isRaised)
        {
            RaiseDoor();
        }
        else if (distanceToPlayer > closeDistance && isRaised)
        {
            LowerDoor();
        }
    }

    void RaiseDoor()
    {
        // Interpolăm poziția cubului ușii de la poziția inițială la poziția ridicată
        door.position = Vector3.Lerp(door.position, raisedPosition, Time.deltaTime * doorSpeed);
        isRaised = true;
    }

    void LowerDoor()
    {
        // Interpolăm poziția cubului ușii de la poziția ridicată la poziția inițială
        door.position = Vector3.Lerp(door.position, initialPosition, Time.deltaTime * doorSpeed);
        isRaised = false;
    }
}