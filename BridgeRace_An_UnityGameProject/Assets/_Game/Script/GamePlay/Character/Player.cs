using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : AnCharacter
{
    public DynamicJoystick joyStick;
    [SerializeField] private float moveSpeed;
    
    public override void OnInit()
    {
        joyStick.gameObject.SetActive(true);
        ChangeColor(color);
    }

    private void Update()
    {
        if(GameManager.Ins.IsState(GameState.GamePlay))
        {
            isGround = CheckGround();
            Vector3 inputJoyStick = new Vector3(joyStick.Horizontal, 0f, joyStick.Vertical);

            if (inputJoyStick.magnitude > 0f)
            {
                ChangeAnim("run");
                Move(inputJoyStick);
            }
            else
            {
                ChangeAnim("idle");
            }

            //CheckIsUp
            if (transform.forward.z > 0f)
            {
                isUp = true; // Di len cau
            }
            else if (transform.forward.z < 0f)
            {
                isUp = false; // Di xuong cau
                moveSpeed = 5;// Di xuong cau -> Luon di duoc
            }
        }
        

    }

    private void Move(Vector3 direct)
    {
        agent.enabled = true;
        Vector3 targetPos = Camera.main.transform.TransformDirection(direct);
        targetPos.y = 0f;

        agent.Move(moveSpeed * Time.deltaTime * targetPos);

        if (targetPos != Vector3.zero)
        {
            Quaternion targetRos = Quaternion.LookRotation(targetPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRos, 10f * Time.deltaTime);
        }
    }

    public override void OnStopMove()
    {
        base.OnStopMove();
        agent.enabled = false;
    }

    public override void Win()
    {

        //OnStopMove();
        joyStick.gameObject.SetActive(false);
        base.Win();

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
                                moveSpeed = 0;

                            }
                        }
                    }
                }
            }
        }
    }
}


