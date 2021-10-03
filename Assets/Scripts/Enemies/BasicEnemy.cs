using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicEnemy : Enemy
{

    private Transform target;
    private EnemyState currentState;

    private Rigidbody2D rigidBody;


    [Tooltip("Movement and combat")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackTime;
    private float lastAttack;
    [SerializeField] private float attackDamage;


    [Tooltip("Idle behavior")]
    [SerializeField] private List<Transform> patrolLocs;
    private int currentPatrolLoc;

    [Tooltip("Attacking behavior")]
    [SerializeField] Transform eyeCenter;
    [SerializeField] GameObject attackEye;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.IDLE;
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredVelocity = Vector3.zero;
        switch (currentState)
        {
            case EnemyState.IDLE:
                attackEye.SetActive(false);
                if (patrolLocs != null)
                {
                    target = patrolLocs[currentPatrolLoc];
                    currentPatrolLoc += 1;
                    if (currentPatrolLoc >= patrolLocs.Count)
                    {
                        currentPatrolLoc = 0;
                    }
                    currentState = EnemyState.PATROL;
                }
                break;
            case EnemyState.PATROL:
                attackEye.SetActive(false);
                if (target != null)
                {
                    desiredVelocity = (target.position - transform.position).normalized * moveSpeed;
                }
                if (Vector2.Distance(target.position, transform.position) <= attackRange)
                {
                    currentState = EnemyState.IDLE;
                }
                break;
            case EnemyState.CHASING:
                attackEye.SetActive(true);
                if (target != null)
                {
                    desiredVelocity = (target.position - transform.position).normalized * moveSpeed;
                    UpdateAttackEye();
                }
                if (Vector2.Distance(target.position, transform.position) <= attackRange)
                {
                    currentState = EnemyState.ATTACKING;
                }
                break;
            case EnemyState.ATTACKING:
                if (Time.time - lastAttack >= attackTime)
                {
                    Attack();
                }
                currentState = EnemyState.CHASING;
                break;
            case EnemyState.DEAD:
                break;
        }

        rigidBody.velocity = Vector3.MoveTowards(rigidBody.velocity, desiredVelocity, acceleration * Time.deltaTime);

    }

    private void Attack()
    {
        Player.Instance.stability.TakeDamage(attackDamage);
        lastAttack = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            target = collision.gameObject.transform;
            anim.SetTrigger("chasing");
            currentState = EnemyState.CHASING;
        }
    }

    private void UpdateAttackEye()
    {
        Vector2 targetDist = (target.transform.position - eyeCenter.position).normalized;
        Vector2 pos = targetDist * 0.15f;
        attackEye.transform.localPosition = (Vector2)eyeCenter.localPosition + pos;
    }


    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.DrawSphere(target.position, 0.2f);
        }
        Handles.Label(transform.position + new Vector3(0, 1.0f, 0), currentState.ToString());
    }

    public override void OnAttackedBy(Transform tar)
    {
        target = tar;
        currentState = EnemyState.CHASING;
    }
}
