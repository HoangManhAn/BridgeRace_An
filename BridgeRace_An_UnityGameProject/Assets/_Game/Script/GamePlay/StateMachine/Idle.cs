using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AnIState
{

    float timer;
    float randomTime = 3;

    public void OnEnter(Enemy enemy)
    {
        enemy.ChangeAnim("idle");
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer < randomTime)
        {
            enemy.ChangeState(new SeekBrick());
        }
    }
    public void OnExit(Enemy enemy)
    {
        
    }
}
