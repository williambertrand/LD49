using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerCombat : MonoBehaviour
{
    // Basic shooting / projectiles
    [SerializeField] private float AttackTime;
    [SerializeField] private float lastAttack;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float projectileSpeed;

    [SerializeField] private InputActionReference attackInputReference;

    // Ref to ships rigidbody for use when shooting
    private Rigidbody2D rigidBody;
    private PlayerMovement movement;

    private bool canAttack()
    {
        return (Time.time - lastAttack >= AttackTime);
    }


    private float attackStartTime;
    private bool isAttacking;
    [SerializeField]  private float attackChargeThreshold;
    [SerializeField] private float minChargeThreshold;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();

        attackInputReference.action.performed += context =>
        {
            if(!isAttacking)
            {
                StartAttack();
            } else
            {
                PerformAttack();
            }
        };
    }

    private void Update()
    {
        if (isAttacking && Time.time - attackStartTime >= minChargeThreshold)
        {
            // Show Charging indicator and set fill
        }
    }


    private void StartAttack()
    {
        //if (!canAttack() && !isAttacking) return;
        isAttacking = true;
        attackStartTime = Time.time;
    }

    private void PerformAttack()
    {
        float holdTime = Time.time - attackStartTime;
        if (holdTime < attackChargeThreshold)
        {
            NormalAttack();
        }
        else
        {
            ChargeAttack();
        }
        isAttacking = false;
    }

    private Vector2 VelocityForDir(int dir)
    {
        if (dir == DIR.N)
        {
            return new Vector2(0, projectileSpeed);
        }
        else if (dir == DIR.E)
        {
            return new Vector2(projectileSpeed, 0);
        }
        else if (dir == DIR.S)
        {
            return new Vector2(0, -projectileSpeed);
        }
        else if (dir == DIR.W)
        {
            return new Vector2(-projectileSpeed, 0);
        }
        else
        {
            return new Vector2(0, projectileSpeed);
        }
    }

    private void NormalAttack() {
        Quaternion projectileRot = DIR.rotationForDir(movement.currentDir);
        GameObject pro = Instantiate(projectile, attackPos.position, projectileRot);
        Vector2 projectileVel = VelocityForDir(movement.currentDir);
        pro.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity + projectileVel;
        lastAttack = Time.time;
    }

    private void ChargeAttack()
    {
        //GameObject pro = Instantiate(projectileCharged, attackPos.position, Quaternion.identity);
        //Vector2 projectileVel = transform.up * projectileSpeed * chargeFactor;
        //pro.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity + projectileVel;
        lastAttack = Time.time;
    }
}
