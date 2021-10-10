using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private Transform doorPos;
    [SerializeField] private GameObject Door;

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasSpawned) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(Door, doorPos.position, Quaternion.identity);
            hasSpawned = true;
        }
    }
}
