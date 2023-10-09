using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public Text level;
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
        GameManager.Ins.ChangeState(GameState.Pause);
        Close(0);
    }
}
