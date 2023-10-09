using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : AnCharacter
{
    [SerializeField] protected Transform tf;
    
    public Vector3 destination;
    public bool IsDestionation => Vector3.Distance(tf.position, destination + (tf.position.y - destination.y) * Vector3.up) < 0.1f;

    private AnIState currentState;
    private Stage currentStage;

    public Vector3 winPos;


    public override void OnInit()
    {
        ChangeColor(color);
        ChangeState(new Idle());
    }

    private void Update()
    {
        if(GameManager.Ins.IsState(GameState.GamePlay)) 
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }

            isGround = CheckGround();

            //CheckIsUp
            if (tf.forward.z > 0f)
            {
                isUp = true; // Di len cau
            }
            else if (tf.forward.z < 0f)
            {
                isUp = false; // Di xuong cau
            }
        }
        else
        {
            ChangeState(new Pause());
        }
    }


    public void SetDestination(Vector3 pos)
    {
        agent.enabled = true;
        destination = pos;
        agent.SetDestination(pos);
    }

    public override void OnStopMove()
    {
        agent.enabled = false;
    }

    public void ChangeState(AnIState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public Vector3 SeekBrickSameColor()
    {
        return currentStage.SeekBrick(color);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("WinPoint"))
        {
            if (!other.gameObject.CompareTag("StageBox"))
            {
                if (isGround) //Tren san
                {
                    //Cung mau -> an gach
                    if (other.GetComponent<ColorBrick>().color == color)
                    {
                        other.GetComponent<ColorBrick>().ChangeColor(ColorType.None);
                        AddBrick();
                    }
                }
                else // Tren cau
                {
                    if (isUp) // Di len cau
                    {
                        //Khac mau
                        if (other.GetComponent<ColorBrick>().color != color)
                        {
                            if (brickStack.Count > 0) // Con gach (Stack gach da co san 2 gia tri default)
                            {
                                //Khong mau
                                if (other.GetComponent<ColorBrick>().color != color)
                                {
                                    other.GetComponent<ColorBrick>().ChangeColor(color);
                                    RemoveBrick();
                                }
                                //else// Cac mau con lai
                                //{
                                //    other.GetComponent<ColorBrick>().ChangeColor(color);
                                //}
                            }
                            else // Het Gach
                            {
                                // Bi chan
                                ChangeState(new SeekBrick());

                            }
                        }
                    }
                }

            }
            else
            {
                currentStage = other.gameObject.GetComponent<Stage>();
            }

        }
    }
}


