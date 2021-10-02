using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{

    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Enemy hit = collision.gameObject.GetComponent<Enemy>();
            // direct hits from projectile are "0" chain
            //hit.TakeDamage(damage, 0, true);

        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            //Player.Instance.TakeDamage(damage);
        }
        // This removes the projectile when it hits out board bounds
        // ImpactManager.SpawnImpactAt(impactTag, transform.position)
        gameObject.SetActive(false); // This object is pooled so dont destroy it 
    }
}