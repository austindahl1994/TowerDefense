using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager Instance;
    
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
    }
}


