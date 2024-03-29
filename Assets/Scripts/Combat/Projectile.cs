using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{

    public float damage;

    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HasHealth hit = collision.gameObject.GetComponent<HasHealth>();
            hit.TakeDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.stability.TakeDamage(damage);
        }

        //TODO: ObjectPooler.Instance.Spawn...
        //audioSource.PlayOneShot(wallHit);
        Destroy(gameObject);
    }
}