using UnityEngine;

public class ThisShouldWork : MonoBehaviour
{
    public int roomWidth = 10;
    public int roomHeight = 10;
    public float tileSize = 1.0f;
    private GameObject[,] floorTiles;
    private GameObject[] walls;

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
        Vector3 wallStartPosition = transform.position + new Vector3((roomWidth / 2) * tileSize, 0, 0); // Starting position for walls

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
    }

  
}
