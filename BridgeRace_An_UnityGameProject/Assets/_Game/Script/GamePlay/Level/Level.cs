using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    //public NavMeshData navMeshData;
    

    public Transform playerStartPoint;

    public List<Transform> enemyStartPoint = new List<Transform>();

    public Transform winPoint;

    public List<Stage> stages = new List<Stage>();

} 