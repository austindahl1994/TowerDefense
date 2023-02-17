using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    // Tilemap that the turret will be placed on
    [SerializeField] private Tilemap tilemap; 

    // The prefab for the turret that will be placed down
    [SerializeField] private GameObject turretToBePlaced; 

    // The sprite to show where the turret will be placed
    [SerializeField] private SpriteRenderer ghostSprite; 

    //Tile that allows turret to be placed, then tile that it is changed to for visual effect/could remove Dict then?
    [SerializeField] private Tile placeableTurretTile;
    [SerializeField] private Tile placedTile;

    //keeps track of vector3s where turrets are placed so that another one may not be created where one exists
    public Dictionary<Vector3Int, bool> occupiedTiles = new Dictionary<Vector3Int, bool>();

    //standard singleton shenanigans
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        OnMouseDown();
    }

    public void OnMouseDown()
    {
        //checks if there is no turret to be placed, or not in some kind of "build mode"
        if (turretToBePlaced == null) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            //gets position from screen to world for mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //changes mouse position into cell position for working with tiles
            Vector3Int tilePosition = tilemap.WorldToCell(mousePosition);

            //gets tile that is clicked on to check if tile is able to have a turret placed on it
            TileBase tileForTurret = tilemap.GetTile(tilePosition);

            //Checks Dict to see if tile exists at key location, can change prolly
            if (!occupiedTiles.ContainsKey(tilePosition) && (tileForTurret == placeableTurretTile)) { 
                PlaceNewTurretDown(tilePosition);
            }
        }
    }

    public void PlaceNewTurretDown(Vector3Int tilePos) {
        //Changes tile from a tile that a turret can be placed on to one that cant, can remove Dict now? also visual change
        tilemap.SetTile(tilePos, placedTile);

        //Spawns turret it
        Instantiate(turretToBePlaced, tilemap.GetCellCenterWorld(tilePos), Quaternion.identity);

        //Adds location to Dict, one method of making sure a tile cannot have two turrets on it
        occupiedTiles.Add(tilePos, true);

        //Makes it so that no more of that turret can be spawn in, UI can set new turret to spawn in
        //turretToBePlaced = null;
    }

    //This will be to remove turrets, destroy turret object and either remove from Dict or change tile
    //back to a tile that a turret can be placed on
    public void RemoveTurret(Vector3 tilePos) { 
        
    }
}
