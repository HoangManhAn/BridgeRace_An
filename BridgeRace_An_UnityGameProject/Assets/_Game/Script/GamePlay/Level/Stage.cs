

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stage : MonoBehaviour
{

    [SerializeField] private Brick brickStage;
    [SerializeField] private Transform brickPrefabHolder;
    public List<Brick> brickInStage = new List<Brick>();
    

    [SerializeField] private Transform brickDefault;

    public List<int> colorNumber = new List<int>();

    private int[,] BrickCoordinate = new int[8, 8];
    

    private void Start()
    {
        LoadMap();
        InvokeRepeating(nameof(RestoreMap), 0f, 3f);
    }

    private void LoadMap()
    {

        for (int i = 0; i < BrickCoordinate.GetLength(0); i++)
        {
            for (int j = 0; j < BrickCoordinate.GetLength(1); j++)
            {

                brickStage.ChangeColor(ColorType.None);
                InstantiateBrick(i, j);

            }
        }
    }

    public void RestoreMap()
    {
        if( brickInStage.Count >  0)
        {
            for (int i = 0; i < brickInStage.Count; i++)
            {
                int rand = (int)Random.Range(0.5f, 4.5f);

                if (CheckColorBrickInStage(rand))
                {
                    if (rand == 1)
                    {
                        brickInStage[i].ChangeColor(ColorType.Blue);
                    }

                    if (rand == 2)
                    {

                        brickInStage[i].ChangeColor(ColorType.Red);
                    }

                    if (rand == 3)
                    {
                        brickInStage[i].ChangeColor(ColorType.Green);
                    }

                    if (rand == 4)
                    {
                        brickInStage[i].ChangeColor(ColorType.Orange);
                    }
                }

            }
        }
    }

    public bool CheckColorBrickInStage(int rand)
    {
        if (colorNumber.Count > 0)
        {
            for (int i = 0; i < colorNumber.Count; i++)
            {
                if (rand == colorNumber[i])
                {
                    return true;
                }
            }
        }
        return false;
    }


    private void InstantiateBrick(int i, int j)
    {

        Brick brickPrefab = Instantiate(brickStage, brickDefault.position + new Vector3(i, 0.1f, -j), Quaternion.Euler(new Vector3(0f, 90, 0)));
        brickPrefab.transform.parent = this.brickPrefabHolder;
        brickInStage.Add(brickPrefab);
    }

    public void ClearBrickInStage()
    {
        if (brickInStage.Count > 0 )
        {
            for (int i = 0; i < brickInStage.Count; i++)
            {
                Destroy(brickInStage[i].gameObject);
            }
            brickInStage.Clear();
        }    
    }

    public Vector3 SeekBrick(ColorType color)
    {
        //if (brickInStage.Count > 0)
        //{
        //    for (int i = 0; i < brickInStage.Count; i++)
        //    {
        //        if (color == brickInStage[i].color)
        //        {
        //            return brickInStage[i].transform.position;
        //        }
        //    }
        //}

        if (brickInStage.Count > 0)
        {

            Transform brickNearest = brickInStage[0].transform;

            float dis;
            float disMin = Vector3.Distance(transform.position, brickNearest.position);
            for (int i = 0; i < brickInStage.Count; i++)
            {
                if (brickInStage[i] == null) continue;
                if (color == brickInStage[i].color)
                {
                    dis = Vector3.Distance(transform.position, brickInStage[i].transform.position);
                    if (dis < disMin)
                    {
                        disMin = dis;
                    }
                }
                
            }

            for (int i = 0; i < brickInStage.Count; i++)
            {
                if (brickInStage[i] == null) continue;
                if(color == brickInStage[i].color)
                {
                    dis = Vector3.Distance(transform.position, brickInStage[i].transform.position);
                    if (Mathf.Abs(dis - disMin) <= 0.1f)
                    {
                        return brickInStage[i].transform.position;
                    }
                }
                
            }
        }

        //return targets[0].TF;
        //return Vector3.zero;
        return brickInStage[(int)(brickInStage.Count/2)].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag is "Enemy" or "Player")
        {
            
            if (other.GetComponent<AnCharacter>().color == ColorType.Blue)
            {
                
                colorNumber.Add(1);
            }

            if (other.GetComponent<AnCharacter>().color == ColorType.Red)
            {
                    
                colorNumber.Add(2);
            }

            if (other.GetComponent<AnCharacter>().color == ColorType.Green)
            {

                colorNumber.Add(3);
            }

            if (other.GetComponent<AnCharacter>().color == ColorType.Orange)
            {
                
                colorNumber.Add(4);
            }

        }
        
    }



}

