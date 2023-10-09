using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBrick : AnIState
{
    float time = 0f;
    float counterTime = 3f;
    int rand = (int)Random.Range(0.5f, 10.5f);
    public void OnEnter(Enemy enemy)
    {
        time += Time.deltaTime;

        if (time > counterTime)
        {
            enemy.ChangeAnim("run");
            enemy.SetDestination(enemy.SeekBrickSameColor());
        }
    }

    public void OnExecute(Enemy enemy) 
    {
       
        if (enemy.brickStack.Count > rand) 
        {
            enemy.ChangeState(new BuildBrick());
        }
        else
        {
            enemy.ChangeAnim("run");
            enemy.SetDestination(enemy.SeekBrickSameColor());
        }
    }

    public void OnExit(Enemy enemy)
    {
        if(GameManager.Ins.IsState(GameState.GamePlay))
        {
            if (enemy.IsDestionation)
            {
                enemy.ChangeState(new SeekBrick());
            }
        }
        
        
    }
}
