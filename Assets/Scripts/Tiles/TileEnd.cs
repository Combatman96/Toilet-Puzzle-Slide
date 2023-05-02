using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEnd : BaseTile
{
    public Direction inDirection;
    [SerializeField] private List<TileType> m_listConnectableTilesIn;

    public override List<TileType> GetConnectableTiles(Direction direction)
    {
        if(direction == inDirection)
            return m_listConnectableTilesIn;
        return null;
    }
}
