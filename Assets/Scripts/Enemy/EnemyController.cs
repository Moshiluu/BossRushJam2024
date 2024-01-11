using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    GameObject player;

    NavMeshAgent agent;
    [SerializeField]
    LayerMask groundLayer, playerLayer;

    Vector3 destPoint;
    bool walkpointSet = false;
    [SerializeField]
    float walkRange;

    [SerializeField]
    float sightRange, attackRange;
    [SerializeField]
    bool playerInSightRange, playerInAttackRange;
    Animator anim;
    public BoxCollider boxCollider;

    public Health health;
    public float healthAmount;
    private float lastAttack;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        lastAttack = Time.time;
    }

    void Update()
    {
        healthAmount = health.HP;
        
        if (health.HP <= 0)
        {
            Destroy(gameObject);
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        if(playerInSightRange && playerInAttackRange)
        {
            Attack();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }
        else if(!playerInSightRange && !playerInAttackRange)
        {
            Patrol();
        }

        if (agent.hasPath)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void Attack()
    {
        if (!anim.GetNextAnimatorStateInfo(0).IsName("Attack") && Time.time - lastAttack > 1.0f)
        {
            anim.SetTrigger("Attack");
            if(agent.isActiveAndEnabled)
            {
                agent.SetDestination(transform.position);
            }
            lastAttack = Time.time;
        }

    }
    void Patrol()
    {
        if(!walkpointSet)
        {
            SearchForDest();
        }

        if(walkpointSet)
        {
            if (agent.isActiveAndEnabled)
            {
                agent.SetDestination(destPoint);
            }
 
        }

        if(Vector3.Distance(transform.position,destPoint) < 10)
        {
            walkpointSet = false;
        }
    }

    void Chase()
    {
        if(agent.isActiveAndEnabled)
        {
          agent.SetDestination(player.transform.position);
        }

    }

    void SearchForDest()
    {
        float z = Random.Range(-walkRange, walkRange);
        float x = Random.Range(-walkRange, walkRange);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destPoint,Vector3.down, groundLayer))
        {
            walkpointSet=true;
        }
    }

    public void EnableAttack()
    {
        boxCollider.enabled=true;
    }

    public void DisableAttack()
    {
        boxCollider.enabled=false;
    }

}
