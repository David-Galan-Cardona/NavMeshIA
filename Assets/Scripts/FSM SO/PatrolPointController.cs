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

        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyController.TakeDamage(1);
            Pool.Instance.Return(collision.gameObject);

        }
    }
}
