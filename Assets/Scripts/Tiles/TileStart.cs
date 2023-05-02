using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStart : BaseTile
{
    public Direction outDirection;
    [SerializeField] List<TileType> m_listConnectableTilesOut;

    public override List<TileType> GetConnectableTiles(Direction direction)
    {
        if(direction == outDirection)
            return m_listConnectableTilesOut;
        return null;
    }
}
