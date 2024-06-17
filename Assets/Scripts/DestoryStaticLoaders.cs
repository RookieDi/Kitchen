using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryStaticLoaders : MonoBehaviour
{
    private void Awake()
    {
        CuttinCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashBin.ResetStaticData();
    }
}
