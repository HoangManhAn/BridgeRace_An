using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBrick : AnIState
{
    
    public void OnEnter(Enemy enemy)
    {
        //enemy.SetDestination(enemy.winPoint.position);
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.SetDestination(enemy.winPos);
    }
    public void OnExit(Enemy enemy)
    { 

    }
}
