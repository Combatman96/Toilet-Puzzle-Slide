using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public GridMap gridMap;
    public Stickman stickman;

    // Start is called before the first frame update
    void Start()
    {
        OnNewGame();
    }

    void OnNewGame()
    {
        gridMap.SetUpGridMap();
        stickman.SetState(Stickman.s_idle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
