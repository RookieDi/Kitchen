using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManualGenerator : MonoBehaviour
{
  [SerializeField] private GameObject blackCube, whiteCube, greenCube;
  [SerializeField] private GameObject romOne, romTwo, romThree;
  private float distanceBetweenTiles = 1f;

[ContextMenu("GenerateCubesRoom1")]
  private void GenerateCubesRoom1()
  {
    for (int i = 0; i < 10; i++)
    {
      Vector3 newPosition = new Vector3(0, 0, i * distanceBetweenTiles);
      Instantiate(whiteCube,newPosition,Quaternion.identity,romOne.transform);
    }
  }
}
