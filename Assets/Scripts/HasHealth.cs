using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{

    [SerializeField] Enemy enemyBehavior;
    [SerializeField] float maxHealth;
    private float currentHealth;

    [SerializeField] int soulDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyBehavior = GetComponent<Enemy>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            OnDeath();
        }

        if(enemyBehavior != null)
        {
            enemyBehavior.SetTarget(Player.Instance.transform);
        }
    }

    void OnDeath()
    {
        DropSouls();
        Destroy(gameObject);
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
}
