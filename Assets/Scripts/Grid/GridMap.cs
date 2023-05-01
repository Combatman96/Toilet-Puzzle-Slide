using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridMap : MonoBehaviour
{
    private Dictionary<Vector2Int, BaseTile> m_gridMap;

    public void SetUpGridMap()
    {
        m_gridMap.Clear();

        Node[] nodes = GetComponentsInChildren<Node>();
        for (int i = 0; i < nodes.Length; i++)
        {
            Node node = nodes[i];
            BaseTile tile = node.GetComponentInChildren<BaseTile>();
            m_gridMap.Add(node.coordinate, tile);
        }
    }

    public void UpdateGridMap()
    {
        SetUpGridMap();
    }

    public Vector2Int GetTileCoordinate(BaseTile tile)
    {
        return m_gridMap.FirstOrDefault(x => x.Value == tile).Key;
    }

    private Vector2Int[] GetSurroundingSteps(BaseTile tile)
    {
        return GetSurroundingSteps(tile.GetTileType());
    }

    private Vector2Int[] GetSurroundingSteps(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Start_Up:
                return new Vector2Int[] { Vector2Int.up };
            case TileType.Start_Down:
                return new Vector2Int[] { Vector2Int.down };
            case TileType.Start_Left:
                return new Vector2Int[] { Vector2Int.left };
            case TileType.Start_Right:
                return new Vector2Int[] { Vector2Int.right };

            case TileType.Straight_Vertical:
                return new Vector2Int[] { Vector2Int.down, Vector2Int.up };
            case TileType.Straight_Horizontal:
                return new Vector2Int[] { Vector2Int.left, Vector2Int.right };

            case TileType.Curve_Down_Right:
                return new Vector2Int[] { Vector2Int.down, Vector2Int.right };
            case TileType.Curve_Left_Down:
                return new Vector2Int[] { Vector2Int.left, Vector2Int.down };
            case TileType.Curve_Up_Left:
                return new Vector2Int[] { Vector2Int.up, Vector2Int.left };
            case TileType.Curve_Right_Up:
                return new Vector2Int[] { Vector2Int.right, Vector2Int.up };
        }
        return new Vector2Int[] { new Vector2Int(0, 0) };
    }

    private Direction GetStepDirection(Vector2Int step)
    {
        if(step == Vector2Int.up)
            return Direction.Up;
        if(step == Vector2Int.down)
            return Direction.Down;
        if(step == Vector2Int.left)
            return Direction.Left;
        if(step == Vector2Int.right)
            return Direction.Right;
        return Direction.Still;
    }

    private Dictionary<Vector2Int, BaseTile> m_visitedTiles;

    public Dictionary<Vector2Int, BaseTile> GetPathTile()
    {
        return m_visitedTiles;
    }

    public bool IsHavePath()
    {
        m_visitedTiles.Clear();
        Vector2Int startCoord = m_gridMap.FirstOrDefault(x => x.Value.IsStartTile()).Value.GetCoordinate();
        return IsHavePath(startCoord);
    }

    private bool IsHavePath(Vector2Int coord) //dfs
    {
        BaseTile currentTile = m_gridMap[coord];
        if(currentTile.IsEndTile())
        {
            m_visitedTiles.Add(coord, currentTile);
            return true;
        }    

        m_visitedTiles.Add(coord, currentTile);
        Vector2Int[] steps = GetSurroundingSteps(currentTile);
        foreach(Vector2Int step in steps)
        {
            Vector2Int nextCoord = coord + step;
            if(nextCoord.x >=0 && nextCoord.y < 4) // check out of bound
            {
                var nextTile = m_gridMap[nextCoord];
                if(nextTile == null)
                    continue;
                bool isVisited = m_visitedTiles.ContainsKey(nextCoord);
                var connectableTiles = currentTile.GetConnectableTiles(GetStepDirection(step));
                bool canStepToNextTile = connectableTiles.Contains(nextTile.GetTileType());
                if(!isVisited && canStepToNextTile)
                {
                    if(IsHavePath(nextCoord)) return true;
                }
            }
        }

        return false;
    }
}
