using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public GridMap gridMap;
    public Stickman stickman;
    [SerializeField] private BaseTile m_selectedTile;
    public LayerMask tileLayer;
    [SerializeField] private float m_dragThreadhold = 0.3f;
    [SerializeField] private Transform m_cursor;
    public bool hasPaper = false;

    // Start is called before the first frame update
    void Start()
    {
        OnNewGame();
    }

    void OnNewGame()
    {
        m_selectedTile = null;
        m_cursor.transform.position = new Vector3(600f, 0, 0);
        hasPaper = false;
        gridMap.SetUpGridMap();
        stickman.SetState(Stickman.s_idle);
    }

    // Update is called once per frame
    void Update()
    {
        var touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero, 100f, tileLayer);
            if(hit) 
            {
                m_selectedTile = hit.transform.GetComponent<BaseTile>();
                m_cursor.transform.position = m_selectedTile.transform.position;
            }
        }
        if(Input.GetMouseButtonUp(0) && m_selectedTile)
        {
            Vector2 selectedTilePos = m_selectedTile.transform.position;
            if( Vector2.Distance(touchPos, selectedTilePos) >= m_dragThreadhold)
            {
                gridMap.SlideTile(m_selectedTile, GetDirection(selectedTilePos, touchPos));
            }
            m_selectedTile = null;
            m_cursor.transform.position = new Vector3(600f, 0, 0);
        }

    }

    private Vector2Int GetDirection(Vector2 oldPos, Vector2 newPos)
    {
        Vector2 dir = newPos - oldPos;
        if(Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
        {
            dir.y = 0;
        }
        else 
        {
            dir.x = 0;
        }
        return new Vector2Int( (int) dir.normalized.x, (int) dir.normalized.y);
    }

    private void CheckPath()
    {
        if(gridMap.IsHavePath())
        {
            var pathTile = gridMap.GetPathTile();
            Vector2[] path = pathTile.Select(x => (Vector2) x.transform.position).ToArray();
            stickman.MoveAlongPath(path, 1.2f);
            stickman.SetState(Stickman.s_run);
            gridMap.ClearNonPath();
        }
    }

    public void OnTileSlideCompleted()
    {
        CheckPath();
    }

    public void OnReachGoal()
    {
        int state = (hasPaper) ? Stickman.s_happy : Stickman.s_noPaper;
        stickman.SetState(state);
        StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3.5f);
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
