using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEnemy : Enemy
{


    [SerializeField] private float attackTime;
    [SerializeField] private float angleDelta;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private long shotNum;

    [SerializeField] private GameObject projectile;
    private bool active = true;


    [Tooltip("Movment behavior")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private List<Transform> patrolLocs;
    private int currentPatrolLoc;
    private Transform dest;

    private EnemyState currentState;
    private Rigidbody2D rigidBody;

    void Start()
    {
        StartCoroutine(ShootAndSpin());
        currentState = EnemyState.IDLE;
        rigidBody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

        base.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredVelocity = Vector3.zero;
        switch (currentState)
        {
            case EnemyState.IDLE:
                if (patrolLocs != null && patrolLocs.Count > 0)
                {
                    dest = patrolLocs[currentPatrolLoc];
                    currentPatrolLoc += 1;
                    if (currentPatrolLoc >= patrolLocs.Count)
                    {
                        currentPatrolLoc = 0;
                    }
                }
                currentState = EnemyState.PATROL;
                break;
            case EnemyState.PATROL:
                if (dest != null)
                {
                    desiredVelocity = (dest.position - transform.position).normalized * moveSpeed;
                    if (Vector2.Distance(dest.position, transform.position) <= 0.5f)
                    {
                        currentState = EnemyState.IDLE;
                    }
                }
                break;
            case EnemyState.DEAD:
                break;
        }

        rigidBody.velocity = Vector3.MoveTowards(rigidBody.velocity, desiredVelocity, acceleration * Time.deltaTime);
    }


    IEnumerator ShootAndSpin()
    {
        while(active)
        {
            yield return new WaitForSeconds(attackTime);

            Vector2 vel = ((Vector2)transform.up).Rotate(angleDelta * shotNum);
            GameObject proj = Instantiate(projectile, (Vector2)transform.position + (vel * 0.5f), Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = vel * projectileSpeed;
            shotNum += 1;

        }
    }
}
