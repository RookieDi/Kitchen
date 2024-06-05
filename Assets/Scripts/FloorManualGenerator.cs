using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int gridSize = 5; // Size of the grid (number of cubes along each axis)
    public GameObject wallPrefab; // Prefab for the walls
    public GameObject blackCubePrefab; // Prefab for the black cube
    public GameObject whiteCubePrefab; // Prefab for the white cube

    [ContextMenu("Generate Room")]
    void GenerateRoom()
    {
      
        // Generate floor
        GenerateFloor();

        // Generate walls
        GenerateWalls();
    }

    void GenerateFloor()
    {
        float cubeSpacing = 1f; // Spacing between cubes
        Vector3 floorStartPosition = transform.position - new Vector3(gridSize * cubeSpacing / 2f, 0f, gridSize * cubeSpacing / 2f);

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                GameObject cubePrefab = (x + z) % 2 == 0 ? whiteCubePrefab : blackCubePrefab;
                Vector3 cubePosition = floorStartPosition + new Vector3(x * cubeSpacing, 0f, z * cubeSpacing);
                Instantiate(cubePrefab, cubePosition, Quaternion.identity);
            }
        }
    }

    void GenerateWalls()
    {
        float cubeSpacing = 1f; 
        Vector3 wallStartPosition = transform.position - new Vector3(gridSize * cubeSpacing / 2f, 0f, gridSize * cubeSpacing / 2f);

        for (int i = 0; i < 4; i++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    Vector3 cubePosition = wallStartPosition + new Vector3(x * cubeSpacing, i * cubeSpacing, z * cubeSpacing);

                    if (i == 0)
                    {
                        GameObject cube = Instantiate(blackCubePrefab, cubePosition, Quaternion.identity);
                    }
                    else
                    {
                        if (x == 0 || x == gridSize - 1 || z == 0 || z == gridSize - 1)
                        {
                            GameObject cube = Instantiate(blackCubePrefab, cubePosition, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

   
}
