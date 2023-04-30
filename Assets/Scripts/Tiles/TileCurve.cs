using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCurve : BaseTile
{
    public Direction intDirection;
    [SerializeField] List<TileType> m_otherConnectedTiles;

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

    public List<TileType> GetConenectedTilesIn()
    {
        return m_otherConnectedTiles;
    }
}
