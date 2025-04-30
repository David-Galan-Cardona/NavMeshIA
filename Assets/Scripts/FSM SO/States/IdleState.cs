using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleState", menuName = "StatesSO/Idle")]
public class IdleState : StateSO
{
    public override void OnStateEnter(EnemyController ec)
    {
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        Debug.Log("Here chillin");
        //ves al mas cercano de entre ec.PatrolA y ec.PatrolB
        ec._chaseB.agent.SetDestination(ec.CurrentNodeObject.transform.position);
    }
}
