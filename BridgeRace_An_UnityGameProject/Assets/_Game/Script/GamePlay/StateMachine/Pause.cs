using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : AnIState
{
    public void OnEnter(Enemy enemy)
    {
        enemy.OnStopMove();
    }

    public void OnExecute(Enemy enemy)
    {
        if(!GameManager.Ins.IsState(GameState.Pause))
        {
            enemy.ChangeState(new SeekBrick());
        }
    }

    public void OnExit(Enemy enemy) { }
}
