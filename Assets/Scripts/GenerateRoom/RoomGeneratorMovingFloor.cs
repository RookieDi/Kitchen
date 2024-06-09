using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneratorMovingFloor : MonoBehaviour
{
    public int width = 10;
    public int length = 10;
    public float tileSize = 1.0f;
    public float wallHeight = 3.0f;
    public Transform player;  // Reference to the player

    private GameObject[,] floorTiles;

    [ContextMenu("Generate Room")]
    public void GenerateRoom()
    {
        ClearRoom();
        GenerateFloor();
        GenerateWalls();
    }

    [ContextMenu("Clear Room")]
    public void ClearRoom()
    {
        // Destroy all child objects of this GameObject
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    void GenerateFloor()
    {
        floorTiles = new GameObject[width, length];
        GameObject floorParent = new GameObject("Floor");
        floorParent.transform.parent = transform;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.transform.position = new Vector3(x * tileSize, 0, z * tileSize);
                tile.transform.localScale = new Vector3(tileSize, 0.1f, tileSize);

                // Ensure each tile has a collider
                if (tile.GetComponent<Collider>() == null)
                {
                    tile.AddComponent<BoxCollider>();
                }

                if ((x + z) % 2 == 0)
                {
                    tile.GetComponent<Renderer>().material.color = Color.white;
                }
                else
                {
                    tile.GetComponent<Renderer>().material.color = Color.black;
                }

                tile.transform.parent = floorParent.transform;
                floorTiles[x, z] = tile;
            }
        }
    }

    void GenerateWalls()
    {
        GameObject wallsParent = new GameObject("Walls");
        wallsParent.transform.parent = transform;

        // Create the four walls
        CreateWall(new Vector3(width * tileSize / 2 - tileSize / 2, wallHeight / 2, -tileSize / 2),
                   new Vector3(width * tileSize, wallHeight, tileSize), wallsParent);
        CreateWall(new Vector3(width * tileSize / 2 - tileSize / 2, wallHeight / 2, length * tileSize - tileSize / 2),
                   new Vector3(width * tileSize, wallHeight, tileSize), wallsParent);
        CreateWall(new Vector3(-tileSize / 2, wallHeight / 2, length * tileSize / 2 - tileSize / 2),
                   new Vector3(tileSize, wallHeight, length * tileSize), wallsParent);
        CreateWall(new Vector3(width * tileSize - tileSize / 2, wallHeight / 2, length * tileSize / 2 - tileSize / 2),
                   new Vector3(tileSize, wallHeight, length * tileSize), wallsParent);
    }

    void CreateWall(Vector3 position, Vector3 scale, GameObject parent)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = position;
        wall.transform.localScale = scale;
        wall.GetComponent<Renderer>().material.color = Color.gray;
        wall.transform.parent = parent.transform;
    }

    void Update()
    {
        if (player != null && floorTiles != null)
        {
            UpdateFloorColorBasedOnPlayerPosition();
        }
    }

    void UpdateFloorColorBasedOnPlayerPosition()
    {
        int playerZ = Mathf.FloorToInt(player.position.z / tileSize);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                if (floorTiles[x, z] == null) continue;  // Ensure the tile exists

                if (z == playerZ)
                {
                    floorTiles[x, z].GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    if ((x + z) % 2 == 0)
                    {
                        floorTiles[x, z].GetComponent<Renderer>().material.color = Color.white;
                    }
                    else
                    {
                        floorTiles[x, z].GetComponent<Renderer>().material.color = Color.black;
                    }
                }
            }
        }
    }
}
