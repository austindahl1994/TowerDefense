using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Figured I would need a tilemap manager singleton, made another script for TowerManager
//Might have to move some code around depending on what we need for each
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


