using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

    public List<Level> levels = new List<Level>();
    Level currentLevel;

    public Player player;
    public List<Enemy> enemy;

    public int currentLevelIndex = 1;


    public void LoadLevel()
    {

        UIManager.Ins.OpenUI<GamePlay>().level.text = "Level " + currentLevelIndex.ToString();
        LoadLevel(currentLevelIndex);
        OnInit();
    }
    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        if (indexLevel <= levels.Count)
        {
            currentLevel = Instantiate(levels[indexLevel - 1]);
        }
        else
        {
            //TODO
        }
    }

    public void OnInit()
    {

        //Player init
        player.agent.enabled = false;
        player.transform.position = currentLevel.playerStartPoint.position;
        player.OnInit();

        //Enemy init
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].transform.position = currentLevel.enemyStartPoint[i].position;
            enemy[i].OnInit();
        }
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].winPos = currentLevel.winPoint.position;
        }
    }

    public void OnStart()
    {
        currentLevelIndex = 1;
        OnReset();
        LoadLevel();
    }

    public void OnFinish()
    {
        OnReset();
        UIManager.Ins.CloseUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.Finish);
    }

    public void OnNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex <= levels.Count)
        {
            for(int i = 0; i < enemy.Count; i++)
            {
                enemy[i].agent.speed *= 1.5f;
            }
            LoadLevel();
        }
        else
        {
            currentLevelIndex--;
            UIManager.Ins.CloseUI<GamePlay>();
            UIManager.Ins.OpenUI<ComingSoon>();
            GameManager.Ins.ChangeState(GameState.Pause);

        }
    }

    public void OnRetry()
    {
        OnReset();
        LoadLevel();
    }

    public void OnReset()
    {
        //CLear BrickInStage
        if (currentLevel != null)
        {
            for (int i = 0; i < currentLevel.stages.Count; i++)
            {
                currentLevel.stages[i].ClearBrickInStage();
            }
        }


        //Clear BrickStack
        player.ClearBrick();
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].ClearBrick();
        }
    }
}
