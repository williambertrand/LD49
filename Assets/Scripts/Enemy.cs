using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{

    private enum STATE
    {
        IDLE,
        PATROL,
        CHASING,
        ATTACKING,
        DEAD
    }

    private Transform target;
    private STATE currentState;

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

    // Start is called before the first frame update
    void Start()
    {
        currentState = STATE.IDLE;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredVelocity = Vector3.zero;
        switch (currentState)
        {
            case STATE.IDLE:
                if(patrolLocs != null)
                {
                    target = patrolLocs[currentPatrolLoc];
                    currentPatrolLoc += 1;
                    if(currentPatrolLoc >= patrolLocs.Count)
                    {
                        currentPatrolLoc = 0;
                    }
                    currentState = STATE.PATROL;
                }
                break;
            case STATE.PATROL:
                if (target != null)
                {
                    desiredVelocity = (target.position - transform.position).normalized * moveSpeed;
                }
                if (Vector2.Distance(target.position, transform.position) <= attackRange)
                {
                    currentState = STATE.IDLE;
                }
                break;
            case STATE.CHASING:
                if(target != null)
                {
                    desiredVelocity = (target.position - transform.position).normalized * moveSpeed;
                }
                if(Vector2.Distance(target.position, transform.position) <= attackRange)
                {
                    currentState = STATE.ATTACKING;
                }
                break;
            case STATE.ATTACKING:
                if(Time.time - lastAttack >= attackTime)
                {
                    Attack();
                }
                currentState = STATE.CHASING;
                break;
            case STATE.DEAD:
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
            currentState = STATE.CHASING;
        }
    }


    private void OnDrawGizmos()
    {
        if(target != null)
        {
            Gizmos.DrawSphere(target.position, 0.2f);
        }
        Handles.Label(transform.position + new Vector3(0, 1.0f, 0), currentState.ToString());
    }

    public void SetTarget(Transform tar)
    {
        target = tar;
        currentState = STATE.CHASING;
    }
}
