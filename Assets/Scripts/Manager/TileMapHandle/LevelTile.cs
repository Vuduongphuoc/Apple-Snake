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
    Border = 1,
    Apple = 3,
    Spike = 4,
    //Unit
    Worm = 1000,

}