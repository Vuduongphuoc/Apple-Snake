    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = " New Level Tile", menuName = "2D/Tiles/Level Tile")]
public class LevelTile : Tile
{
    public TileType type;
}

[SerializeField]
public enum TileType
{
    //Ground
    Floor = 0,
    Border = 1000,
    Apple = 3000,
    Spike = 4000,
    //Unit
    Worm = 10000,

}