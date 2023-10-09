using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingSoon : UICanvas
{
    public void MainMenuButton()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
        Close(0);
    }

    public void RetryButton()
    {
        GameManager.Ins.ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<GamePlay>();
        LevelManager.Ins.OnRetry();
        Close(0);
    }
}
