using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AnCharacter>().Win();
            GameManager.Ins.ChangeState(GameState.Finish);
            LevelManager.Ins.OnFinish();
            UIManager.Ins.OpenUI<Win>();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AnCharacter>().Win();
            GameManager.Ins.ChangeState(GameState.Finish);
            LevelManager.Ins.OnFinish();
            UIManager.Ins.OpenUI<Lose>();
        }
            
    }
}
