using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyController : HPInterface
{
    public GameObject target;
    public bool OnRange = false, OnAttackRange = false;
    public ChaseBehaviour _chaseB;
    public Animator animator;
    public StateSO currentNode;
    public List<StateSO> Nodes;
    public GameObject PatrolA;
    public GameObject PatrolB;
    public GameObject CurrentNodeObject;
    public int radiusSearch = 10;
    public EnemyVision enemyVision;

    void Start()
    {
        MaxHP = 10;
        HP = MaxHP;
        _chaseB = GetComponent<ChaseBehaviour>();
        animator.SetBool("Walking", true);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            OnRange = true;
            CheckEndingConditions();
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnRange = false;
            CheckEndingConditions();
            if (HP > MaxHP/2 && HP < MaxHP)
            {
                //busca alrededor de la colision y coloca patrolA y patrolB en puntos aleatorios en un circulo alrededor de la colision
                PatrolA.transform.position = new Vector3(collision.transform.position.x + UnityEngine.Random.Range(-radiusSearch, radiusSearch), collision.transform.position.y, collision.transform.position.z + UnityEngine.Random.Range(-radiusSearch, radiusSearch));
                PatrolB.transform.position = new Vector3(collision.transform.position.x + UnityEngine.Random.Range(-radiusSearch, radiusSearch), collision.transform.position.y, collision.transform.position.z + UnityEngine.Random.Range(-radiusSearch, radiusSearch));
                //si patrolA esta fuera del suelo, lo vuelve a poner aleatoriamente en un circulo alrededor de la colision
                while (PatrolA.transform.position.x>24 || PatrolA.transform.position.x<-24 || PatrolA.transform.position.z>24 || PatrolA.transform.position.z<-24)
                {
                    PatrolA.transform.position = new Vector3(collision.transform.position.x + UnityEngine.Random.Range(-radiusSearch, radiusSearch), collision.transform.position.y, collision.transform.position.z + UnityEngine.Random.Range(-radiusSearch, radiusSearch));
                }
                while (PatrolB.transform.position.x > 24 || PatrolB.transform.position.x < -24 || PatrolB.transform.position.z > 24 || PatrolB.transform.position.z < -24)
                {
                    PatrolB.transform.position = new Vector3(collision.transform.position.x + UnityEngine.Random.Range(-radiusSearch, radiusSearch), collision.transform.position.y, collision.transform.position.z + UnityEngine.Random.Range(-radiusSearch, radiusSearch));
                }
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnAttackRange = true;
            collision.gameObject.GetComponent<HPInterface>().TakeDamage(1);
            CheckEndingConditions();
        }
        /*if (collision.gameObject == PatrolA)
        {
            CurrentNodeObject = PatrolB;
        }
        if (collision.gameObject == PatrolB)
        {
            CurrentNodeObject = PatrolA;
        }*/
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnAttackRange = false;
            CheckEndingConditions();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TakeDamage(1);
        }
        CheckEndingConditions();
        currentNode.OnStateUpdate(this);
    }
    public void CheckEndingConditions()
    {
        foreach (ConditionSO condition in currentNode.EndConditions)
            if (condition.CheckCondition(this) == condition.answer) ExitCurrentNode();
    }
    public void ExitCurrentNode()
    {
        foreach (StateSO stateSO in Nodes)
        {
            if (stateSO.StartCondition == null)
            {
                EnterNewState(stateSO);
                break;
            }
            else
            {
                if (stateSO.StartCondition.CheckCondition(this) == stateSO.StartCondition.answer)
                {
                    EnterNewState(stateSO);
                    break;
                }
            }
        }
        currentNode.OnStateEnter(this);
    }
    private void EnterNewState(StateSO state)
    {
        currentNode.OnStateExit(this);
        currentNode = state;
        currentNode.OnStateEnter(this);
    }

    public override void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            CheckEndingConditions();
        }
    }
}
