using UnityEngine;
using Cinemachine;

public class ThisShouldWork : MonoBehaviour
{
    public int roomWidth = 10;
    public int roomHeight = 10;
    public float tileSize = 1.0f;
    private GameObject[,] floorTiles;
    private GameObject[] walls;
    public GameObject player; 

    public CinemachineVirtualCamera existingCamera; 
    public CinemachineVirtualCamera roomCamera; 

    [ContextMenu("Generate Room")]
    public void GenerateRoom()
    {
       
        floorTiles = new GameObject[roomWidth, roomHeight];
        Vector3 floorStartPosition = transform.position - new Vector3((roomWidth / 2) * tileSize, 0, (roomHeight / 2) * tileSize);

       
        for (int x = 0; x < roomWidth; x++)
        {
            for (int z = 0; z < roomHeight; z++)
            {
                GameObject floorTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                floorTile.transform.position = floorStartPosition + new Vector3(x * tileSize, 0, z * tileSize);
                floorTile.transform.localScale = new Vector3(tileSize, 0.1f, tileSize);

                BoxCollider floorCollider = floorTile.AddComponent<BoxCollider>();
                floorCollider.isTrigger = true;

                floorTile.AddComponent<FloorTile>();

                floorTiles[x, z] = floorTile;
            }
        }

        
        walls = new GameObject[3];
        Vector3 wallStartPosition = transform.position + new Vector3((roomWidth / 2) * tileSize, 0, 0);

       
        for (int i = 0; i < 3; i++)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = wallStartPosition;
            wall.transform.localScale = new Vector3(tileSize, 2.0f, roomHeight * tileSize);
            walls[i] = wall;

            BoxCollider wallCollider = wall.AddComponent<BoxCollider>();
            wallCollider.isTrigger = true;

            wallStartPosition.x -= tileSize * (roomWidth + 1);
        }

       
        if (roomCamera != null)
        {
           
            roomCamera.transform.position = transform.position + new Vector3(0, 10, -10);

            
            if (player != null)
            {
                roomCamera.Follow = player.transform;
                roomCamera.LookAt = player.transform;
            }

         
            roomCamera.Priority = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject == player && roomCamera != null)
        {
         
            roomCamera.Priority = 20;

          
            if (existingCamera != null)
            {
                existingCamera.Priority = 10;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject == player && roomCamera != null)
        {
           
            roomCamera.Priority = 10;

          
            if (existingCamera != null)
            {
                existingCamera.Priority = 20;
            }
        }
    }
}
