using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCurve : BaseTile
{

    public Direction inDirection;
    [SerializeField] List<TileType> m_listConnectableTilesIn;
    public Direction outDirection;
    [SerializeField] List<TileType> m_listConnectableTilesOut;


    public override List<Transform> GetWaypoints()
    {
        Stickman stickman = FindObjectOfType<Stickman>();
        if(!stickman)
            return null;

        List<Transform> waypoints = base.GetWaypoints();
        Vector2 stickmanPos = stickman.transform.position;
        Vector2 firstWaypoint = waypoints[0].transform.position;
        Vector2 lastWaypoint = waypoints[waypoints.Count - 1].transform.position;

        if (Vector2.Distance(stickmanPos, firstWaypoint) < Vector2.Distance(stickmanPos, lastWaypoint))
            return waypoints;
        waypoints.Reverse();
        return waypoints;
    }

    public override List<TileType> GetConnectableTiles(Direction direction)
    {
        if(direction == inDirection)
            return m_listConnectableTilesIn;
        if(direction == outDirection)
            return m_listConnectableTilesOut;
        return null;
    }
}
