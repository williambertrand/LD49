using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public int numEnemies;
    public RoomManager roomManager;

    public void OnEnemyDeath(Vector2 position)
    {
        Debug.Log("Death, remaining: " + numEnemies);
        numEnemies -= 1;
        if (numEnemies == 0)
        {
            roomManager.OnRoomComplete(position);
        }
    }

}
