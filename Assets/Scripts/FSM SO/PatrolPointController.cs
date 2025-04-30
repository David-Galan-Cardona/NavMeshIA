using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointController : MonoBehaviour
{
    public EnemyController enemyController;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == enemyController.PatrolA)
        {
            enemyController.CurrentNodeObject = enemyController.PatrolB;
        }
        if (collision.gameObject == enemyController.PatrolB)
        {
            enemyController.CurrentNodeObject = enemyController.PatrolA;
        }
    }
}
