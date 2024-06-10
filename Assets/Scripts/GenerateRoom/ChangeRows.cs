using UnityEngine;

public class ChangeRows : MonoBehaviour
{
    public int roomWidth = 10;
    public int roomHeight = 10;
    public float tileSize = 1.0f;
    private GameObject[,] tiles;
    private bool[,] isWhiteRow;
    private bool rowsGenerated = false;
    private Transform waveTransform; // Transformarea pentru a muta întregul rând
    private Vector3 playerStartPosition;
    private Vector3 playerEndPosition;
    private bool isWaveMoving = false; // Verificăm dacă unda este deja în mișcare

    void Start()
    {
        GenerateRows();
        CreateWaveTransform();
    }

    [ContextMenu("Generate Rows")]
    public void GenerateRows()
    {
        DestroyRows();

        tiles = new GameObject[roomWidth, roomHeight];
        isWhiteRow = new bool[roomWidth, roomHeight];

        Vector3 startPosition = transform.position - new Vector3((roomWidth / 2) * tileSize, 0, (roomHeight / 2) * tileSize);

        // Setăm culorile pentru fiecare cub în funcție de indicele rândului
        for (int x = 0; x < roomWidth; x++)
        {
            for (int z = 0; z < roomHeight; z++)
            {
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.transform.position = startPosition + new Vector3(x * tileSize, 0, z * tileSize);
                tile.transform.localScale = new Vector3(tileSize, 0.1f, tileSize);

                // Adăugăm BoxCollider și îl setăm ca trigger
                BoxCollider collider = tile.AddComponent<BoxCollider>();
                collider.isTrigger = true;

                // Adăugăm scriptul Jhon pentru a gestiona evenimentele de trigger
                Jhon floorTileScript = tile.AddComponent<Jhon>();
                floorTileScript.rowIndex = x;
                floorTileScript.columnIndex = z;
                floorTileScript.changeRows = this;

                // Setăm culoarea inițială în funcție de indicele rândului
                Renderer renderer = tile.GetComponent<Renderer>();
                if (x % 2 == 0) // Alternăm culorile între rânduri
                {
                    renderer.material.color = Color.white;
                    isWhiteRow[x, z] = true;
                }
                else
                {
                    renderer.material.color = Color.black;
                    isWhiteRow[x, z] = false;
                }

                tiles[x, z] = tile;
            }
        }

        rowsGenerated = true;
    }

    private void CreateWaveTransform()
    {
        // Creăm un GameObject gol pentru a reprezenta unda
        GameObject waveObject = new GameObject("Wave");
        waveTransform = waveObject.transform;
    }

    public void WalkedOnTile(int rowIndex, int columnIndex)
    {
        if (rowsGenerated && !isWaveMoving)
        {
            MoveWave(rowIndex);
        }
    }

    private void MoveWave(int rowIndex)
    {
        // Calculăm poziția actuală a playerului și poziția finală a rândului
        playerStartPosition = transform.position;
        playerEndPosition = waveTransform.position;

        // Calculăm direcția de deplasare
        Vector3 moveDirection = (playerStartPosition - playerEndPosition).normalized;

        // Mutăm rândul în direcția calculată
        waveTransform.Translate(moveDirection * Time.deltaTime);

        // Schimbăm culorile între alb și negru pentru cuburile din rând
        for (int z = 0; z < roomHeight; z++)
        {
            Renderer renderer = tiles[rowIndex, z].GetComponent<Renderer>();
            isWhiteRow[rowIndex, z] = !isWhiteRow[rowIndex, z]; // Inversăm culoarea
            renderer.material.color = isWhiteRow[rowIndex, z] ? Color.white : Color.black;
        }

        // Verificăm dacă rândul a ajuns la player
        if (Vector3.Distance(playerStartPosition, waveTransform.position) <= 0.01f)
        {
            isWaveMoving = false; // Oprim mișcarea rândului
        }
    }

    public void DestroyRows()
    {
        if (tiles != null)
        {
            foreach (var tile in tiles)
            {
                Destroy(tile);
            }
            tiles = null;
        }

        if (waveTransform != null)
        {
            Destroy(waveTransform.gameObject);
            waveTransform = null;
        }
    }
}

public class Jhon : MonoBehaviour
{
    public int rowIndex;
    public int columnIndex;
    public ChangeRows changeRows;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeRows.WalkedOnTile(rowIndex,
                columnIndex);
        }
    }
}
