using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        Close(0);
    }

    public void RetryButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        LevelManager.Ins.OnRetry();
        Close(0);
    }
}
