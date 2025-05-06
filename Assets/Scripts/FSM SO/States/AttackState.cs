using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackState", menuName = "StatesSO/Attack")]
public class AttackState : StateSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        ec.animator.SetTrigger("Attack");
        ec.GetComponent<ChaseBehaviour>().StopChasing();
    }

    public override void OnStateExit(EnemyController ec)
    {
        ec.GetComponent<ChaseBehaviour>().Chase(ec.target.transform);
        ec.animator.SetBool("Attack", false);
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        Debug.Log("Te reviento a chancletaso");
    }
}
