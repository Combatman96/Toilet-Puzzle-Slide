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
    [SerializeField] protected List<TileType> m_connectableTiles;

    public void Slide(Vector3 destination, float duration)
    {
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

    public List<TileType> GetConnectableTiles()
    {
        return m_connectableTiles;
    }
}

public enum TileType
{
    Empty = 0,

    Straight_Vertical = 10,
    Straight_Horizontal,
    
    Curve_Down_Right = 20,
    Curve_Left_Down ,
    Curve_Up_Left,
    Curve_Right_Up,

    Start = 30,
    End 
}

public enum SlideDirection
{
    Up,
    Down,
    Left,
    Right
}
