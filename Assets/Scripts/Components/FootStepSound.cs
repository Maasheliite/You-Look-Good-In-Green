using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FootStepSound : MonoBehaviour
{
    public Grid grid;

    void Start()
    {
        if (grid == null)
        {
            grid = FindAnyObjectByType<Grid>();
        }
    }


    // How to check what tile you arte standing on.
    // to be used for footsteps,
    // Add a mapping for each Tile, or whatever you are checking, to a sound,
    // add a coroutine that runs in intervals and
    // let it check the tile and then play the mapped sound.
    void Update()
    {
        foreach (Tilemap map in grid.GetComponentsInChildren<Tilemap>())
        {

            TileBase tile = map.GetTile(Vector3Int.CeilToInt(transform.position));
            if (tile != null)
            {
                string name = tile.name;
                Debug.Log(tile);
                break;
            }

        }
    }
}
