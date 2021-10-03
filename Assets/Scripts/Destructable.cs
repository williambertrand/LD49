using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    BoxCollider2D m_Collider;
    Animator anim;

    private void Start()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private int GetImpactDir(Vector2 thisPos, Vector2 projPos)
    {
        if((thisPos.y + m_Collider.size.y / 2) < projPos.y)
        {
            return 0; // No rotation needed
        }
        else if ((thisPos.y - m_Collider.size.y / 2) > projPos.y)
        {
            return 2;
        }
        else if ((thisPos.x + m_Collider.size.x / 2) < projPos.x)
        {
            return 1;
        }
        else if ((thisPos.x - m_Collider.size.x / 2) > projPos.x)
        {
            return 3;
        }

        return 0;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Get direction the impact was from
            int dir = GetImpactDir(transform.position, collision.gameObject.transform.position);
            Quaternion animRot = DIR.rotationForDir(dir);
            // Rotate by dir

            transform.rotation = animRot;

            // Start animation
            anim.SetTrigger("destroy");
            StartCoroutine(DestroyAfter(0.45f));
        }
    }


    IEnumerator DestroyAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
