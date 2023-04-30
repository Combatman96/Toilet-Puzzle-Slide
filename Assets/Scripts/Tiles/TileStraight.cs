using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStraight : BaseTile
{
    public Direction inDirection;
    [SerializeField] List<TileType> m_otherConnectedTiles;

    public List<TileType> GetConenectedTilesIn()
    {
        return m_otherConnectedTiles;
    }
}
