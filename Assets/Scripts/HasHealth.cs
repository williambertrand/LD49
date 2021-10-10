using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{

    [SerializeField] Enemy enemyBehavior;
    [SerializeField] float maxHealth;
    private float currentHealth;

    [SerializeField] int soulDrop;
    [SerializeField] float deathDelay;
    private bool isDead = false;

    [SerializeField] private AudioClip onDamage;
    private AudioSource audioSource;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyBehavior = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if(onDamage !=null)
        {
            audioSource.PlayOneShot(onDamage);
        }

        if(currentHealth <= 0)
        {
            OnDeath();
        }

        if(enemyBehavior != null)
        {
            enemyBehavior.OnAttackedBy(Player.Instance.transform);
        }
    }

    void OnDeath()
    {
        isDead = true;
        EnemyManager.Instance.OnEnemyDeath(transform.position);
        DropSouls();
        if (anim != null)
        {
            anim.SetTrigger("death");
            StartCoroutine(DestroyAfter(deathDelay));
        } else
        {
            Destroy(gameObject);
        }

    }

    void DropSouls()
    {
        for (int i = 0; i < soulDrop; i++)
        {
            GameObject drop = Instantiate(GamePrefabs.Instance.SoulDrop);
            drop.transform.position = transform.position;

            Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();

            Vector2 randPopVel = new Vector2(
                Random.Range(-2.5f, 2.5f),
                Random.Range(-2.5f, 2.5f)
            );
            dropRB.velocity = randPopVel;
        }
    }

    IEnumerator DestroyAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
