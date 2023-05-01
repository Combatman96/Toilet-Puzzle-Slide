using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class BaseTile : MonoBehaviour
{
    [SerializeField] protected bool m_isMoveable = true;
    [SerializeField] protected TileType m_tileType;
    [SerializeField] private Transform m_wayPointGroup;

    public void Slide(Vector3 destination, float duration)
    {
        if(!m_isMoveable) return;
        transform.DOMove(destination, duration, true).OnComplete(() =>
        {
            //TODO: Call action when the tile have silded to new position
        } 
        );
    }

    public TileType GetTileType()
    {
        return m_tileType;
    }

    public virtual List<Transform> GetWaypoints()
    {
        return m_wayPointGroup.GetComponentsInChildren<Transform>().ToList();
    }

    public virtual List<TileType> GetConnectableTiles(Direction direction)
    {
        return new List<TileType>();
    }

    public Vector2Int GetCoordinate()
    {
        return GetComponentInParent<Node>().coordinate;
    }

    public bool IsStartTile()
    {
        return ((int)m_tileType >= 300 && (int)m_tileType < 400);
    }

    public bool IsEndTile()
    {
        return (int)m_tileType >= 400;
    }
}

public enum TileType
{
    Empty = 0,

    Straight_Vertical = 100,
    Straight_Horizontal,
    
    Curve_Down_Right = 200,
    Curve_Left_Down,
    Curve_Up_Left,
    Curve_Right_Up,

    Start_Up = 300,
    Start_Down,
    Start_Left,
    Start_Right,

    End_Up = 400, 
    End_Down,
    End_Left,
    End_Right
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    Still
}
