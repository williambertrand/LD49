using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDrop : MonoBehaviour
{
    [SerializeField] float amount;
    [SerializeField] float duration;
    private Animator anim;
    private float spawnedAt;

    public string ownerTag;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawnedAt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnedAt >= duration)
        {
            anim.SetTrigger("dissolve");
            StartCoroutine(DestroyAfter(1.0f));
        }
    }

    IEnumerator DestroyAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            //Todo: Collect soul stability
            Player.Instance.stability.Increase(amount);
            Destroy(gameObject);
        }
    }
}
