using System.Collections;
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

    private bool canAttack()
    {
        return (Time.time - lastAttack >= AttackTime);
    }

    private int fireDir;
    private float attackStartTime;
    private bool isAttacking;
    [SerializeField]  private float attackChargeThreshold;
    [SerializeField] private float minChargeThreshold;

    [SerializeField] private AudioClip onShoot;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        attackInputReference.action.performed += AttackPerformed;
    }

    private void AttackPerformed(InputAction.CallbackContext context)
    {
        // Don't shoot while talking
        if (Player.Instance.interaction.isInDialouge) return;

        if (!isAttacking)
        {
            StartAttack(context);
        }
        else
        {
            PerformAttack();
        }
    }



    private void OnDestroy()
    {
        attackInputReference.action.performed -= AttackPerformed;
    }

    private void Update()
    {
        if (isAttacking && Time.time - attackStartTime >= minChargeThreshold)
        {
            // Show Charging indicator and set fill
        }
    }

    private int DirFromKey(string keyName)
    {
        switch (keyName)
        {
            case "upArrow":
                return DIR.N;
            case "downArrow":
                return DIR.S;
            case "rightArrow":
                return DIR.E;
            case "leftArrow":
                return DIR.W;
        }
        return DIR.N;
    }

    private void StartAttack(InputAction.CallbackContext context)
    {
        if (!canAttack()) return;

        fireDir = DirFromKey(context.control.name);
        isAttacking = true;
        attackStartTime = Time.time;
    }

    private void PerformAttack()
    {
        float holdTime = Time.time - attackStartTime;
        if (holdTime < attackChargeThreshold)
        {
            if (Player.Instance.stability.isUnstable)
            {
                StartCoroutine(UnstableAttack());
            } else
            {
                NormalAttack();
            }
        }
        else
        {
            ChargeAttack();
        }
        isAttacking = false;
    }

    private Vector2 VelocityForDir(int dir, float offset)
    {
        if (dir == DIR.N)
        {
            return new Vector2(offset, projectileSpeed + offset);
        }
        else if (dir == DIR.E)
        {
            return new Vector2(projectileSpeed + offset, offset);
        }
        else if (dir == DIR.S)
        {
            return new Vector2(offset, -projectileSpeed + offset);
        }
        else if (dir == DIR.W)
        {
            return new Vector2(-projectileSpeed + offset, offset);
        }
        else
        {
            return new Vector2(0, projectileSpeed);
        }
    }

    private void NormalAttack() {
        if(attackPos == null)
        {
            Debug.Log("no attack pos?");
            attackPos = transform;
        }
        if(audioSource !=null )
        {
            audioSource.PlayOneShot(onShoot);
        }
        Quaternion projectileRot = DIR.rotationForDir(fireDir);
        GameObject pro = Instantiate(projectile, attackPos.position, projectileRot);
        Vector2 projectileVel = VelocityForDir(fireDir, 0);
        pro.GetComponent<Rigidbody2D>().velocity = (rigidBody.velocity * 0.2f) + projectileVel;
        lastAttack = Time.time;
    }

    private void ChargeAttack()
    {
        //GameObject pro = Instantiate(projectileCharged, attackPos.position, Quaternion.identity);
        //Vector2 projectileVel = transform.up * projectileSpeed * chargeFactor;
        //pro.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity + projectileVel;
        lastAttack = Time.time;
    }

    IEnumerator UnstableAttack()
    {
        lastAttack = Time.time;
        int randCount = Random.Range(2, 5);
        for(int i = 0; i < randCount; i++)
        {
            int neg = Random.Range(0.0f, 1.0f) < 0.5f ? -1 : 1;
            float randOffset = Random.Range(2.0f, 5.0f);
            Quaternion projectileRot = DIR.rotationForDir(fireDir);
            GameObject pro = Instantiate(projectile, attackPos.position, projectileRot);
            Vector2 projectileVel = VelocityForDir(fireDir, randOffset * neg);
            pro.GetComponent<Rigidbody2D>().velocity = (rigidBody.velocity * 0.2f) + projectileVel;

            float randFireDelay = Random.Range(0.01f, 0.05f);
            yield return new WaitForSeconds(randFireDelay);
        }
    }
}
