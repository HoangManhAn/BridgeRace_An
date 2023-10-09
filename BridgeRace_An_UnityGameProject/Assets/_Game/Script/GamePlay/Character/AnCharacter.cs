using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnCharacter : MonoBehaviour
{
    public NavMeshAgent agent;


    [SerializeField] ColorData colorData;
    [SerializeField] Renderer meshRenderer;
    public ColorType color;

    [SerializeField] private Animator anim;
    private string currentAnimName;


    [SerializeField] protected LayerMask groundLayer;

    [SerializeField] protected Transform brickStackDefault;
    [SerializeField] protected Brick brickCharacter;
    [SerializeField] protected Transform brickStackPrefabHolder;

    public List<Brick> brickStack = new List<Brick>();


    protected bool isGround;
    protected bool isUp;

    
    public virtual void OnInit()
    {
        //For override
    }

    public virtual void OnStopMove()
    {
        //For override
    }
    

    public void AddBrick()
    {
        brickCharacter.GetComponent<ColorBrick>().ChangeColor(color);
        Brick brickStackPrefab = Instantiate(brickCharacter, brickStackDefault.position + Vector3.up * brickStack.Count * 0.2f, brickStackDefault.rotation /*Quaternion.Euler(new Vector3(0, 90, 0)) */);
        brickStackPrefab.transform.parent = this.brickStackPrefabHolder;
        brickStack.Add(brickStackPrefab);
    }

    public void ClearBrick()
    {
        for(int i = 0; i< brickStack.Count; i++)
        {
            Destroy(brickStack[i].gameObject);
        }
        brickStack.Clear();
    }

    public void RemoveBrick()
    {
        Destroy(brickStack[brickStack.Count - 1].gameObject);
        brickStack.Remove(brickStack[brickStack.Count - 1]);

    }

    public virtual void Win()
    {
        ClearBrick();
        OnStopMove();
        ChangeAnim("win");
    }


    public bool CheckGround()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 2f, groundLayer);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        meshRenderer.material = colorData.GetMat(colorType);
    }
}

