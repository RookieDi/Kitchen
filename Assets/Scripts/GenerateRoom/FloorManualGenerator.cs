using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int gridSize = 5; 
    public GameObject wallPrefab; 
    public GameObject blackCubePrefab; 
    public GameObject whiteCubePrefab;

    [ContextMenu("Generate Room")]
    void GenerateRoom()
    {
      
       
        GenerateFloor();

    
        GenerateWalls();
    }

    void GenerateFloor()
    {
        float cubeSpacing = 1f; 
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
