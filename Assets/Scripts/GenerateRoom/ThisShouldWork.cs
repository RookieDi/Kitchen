using UnityEngine;

public class ThsiShouldWork : MonoBehaviour
{
    public int roomWidth = 10;
    public int roomHeight = 10;
    public float tileSize = 1.0f;
    public float wallHeight = 2.0f; // Adjust this value for taller walls

    private GameObject[,] floorTiles;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found! Please ensure the player object is tagged as 'Player'.");
        }
    }

    [ContextMenu("Generate Room")]
    public void GenerateRoom()
    {
        ClearExistingRoom();

        floorTiles = new GameObject[roomWidth, roomHeight];
        Vector3 startPosition = transform.position - new Vector3((roomWidth / 2) * tileSize, 0, (roomHeight / 2) * tileSize);

        for (int x = 0; x < roomWidth; x++)
        {
            for (int z = 0; z < roomHeight; z++)
            {
                GameObject floorTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                floorTile.transform.position = startPosition + new Vector3(x * tileSize, 0, z * tileSize);
                floorTile.transform.localScale = new Vector3(tileSize, 0.1f, tileSize);

                // Checkerboard pattern
                if ((x + z) % 2 == 0)
                {
                    floorTile.GetComponent<Renderer>().material.color = Color.white;
                }
                else
                {
                    floorTile.GetComponent<Renderer>().material.color = Color.black;
                }

                floorTile.GetComponent<BoxCollider>().enabled = false; // Disable collider so player can walk on it

                floorTiles[x, z] = floorTile;
            }
        }

        // Create walls
        CreateWall(startPosition + new Vector3(-tileSize / 2, wallHeight / 2, roomHeight * tileSize / 2), new Vector3(tileSize, wallHeight, roomHeight * tileSize)); // Left wall
        CreateWall(startPosition + new Vector3(roomWidth * tileSize - tileSize / 2, wallHeight / 2, roomHeight * tileSize / 2), new Vector3(tileSize, wallHeight, roomHeight * tileSize)); // Right wall
        CreateWall(startPosition + new Vector3((roomWidth / 2) * tileSize, wallHeight / 2, -tileSize / 2), new Vector3(roomWidth * tileSize, wallHeight, tileSize)); // Back wall
        CreateWall(startPosition + new Vector3((roomWidth / 2) * tileSize, wallHeight / 2, roomHeight * tileSize - tileSize / 2), new Vector3(roomWidth * tileSize, wallHeight, tileSize)); // Front wall
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.GetComponent<Renderer>().material.color = Color.black;
        wall.transform.parent = transform;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 playerPosition = player.transform.position;
        int x = Mathf.FloorToInt((playerPosition.x - (transform.position.x - (roomWidth / 2) * tileSize)) / tileSize);
        int z = Mathf.FloorToInt((playerPosition.z - (transform.position.z - (roomHeight / 2) * tileSize)) / tileSize);

        if (x >= 0 && x < roomWidth && z >= 0 && z < roomHeight)
        {
            floorTiles[x, z].GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void ClearExistingRoom()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
