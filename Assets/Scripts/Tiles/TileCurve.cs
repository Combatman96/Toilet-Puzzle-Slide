using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCurve : BaseTile
{
    public Direction inDirection;
    [SerializeField] List<TileType> m_listConnectableTilesIn;
    public Direction outDirection;
    [SerializeField] List<TileType> m_listConnectableTilesOut;

    public override List<TileType> GetConnectableTiles(Direction direction)
    {
        if(direction == inDirection)
            return m_listConnectableTilesIn;
        if(direction == outDirection)
            return m_listConnectableTilesOut;
        return null;
    }
}
