using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D _rb;
    public NavMeshAgent agent;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Chase(Transform target)
    {
        //        _rb.velocity = (target.position - self.position).normalized * Speed;
        //muevete hacia el target
        //busca el componente EnemyVision y si playerInSight es true
        if (gameObject.GetComponent<EnemyVision>().playerInSight == true)
        {
            agent.SetDestination(target.position);
        }
    }
    public void Run(Transform target, Transform self)
    {
        Vector3 direction = (self.position - target.position).normalized;
        Vector3 newDestination = self.position + direction * 5f;
        agent.SetDestination(newDestination);
    }

    public void StopChasing()
    {
        agent.SetDestination(transform.position);
    }
    public void PetChase(Transform target)
    {
        agent.SetDestination(target.position);
    }
}
