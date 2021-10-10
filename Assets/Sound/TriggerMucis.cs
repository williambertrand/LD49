using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMucis : MonoBehaviour
{

    [SerializeField] MusicManager musicToTrigger;

    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            musicToTrigger.BeginMusic();
            Player.Instance.GetComponent<PlayerMovement>().SetMoveSpeed(2);
            hasTriggered = true;
        }
    }
}
