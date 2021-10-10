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
        numEnemies -= 1;
        if (numEnemies <= 0 && roomManager != null)
        {
            roomManager.OnRoomComplete(position);
        }
    }

    public void AddEnemy()
    {
        numEnemies += 1;
    }

}
