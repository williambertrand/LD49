using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Size
{
    public int width;
    public int height;
}

public class RoomManager : MonoBehaviour
{

    [SerializeField] private Size roomSize;
    [SerializeField] private GameObject exitPortal;

    private int[] enemiesForLayout;

    // Start is called before the first frame update
    void Start()
    {
        enemiesForLayout = new int[] {
            3,
            2,
            4,
            0, // Store
            5
        };

        // Todo if time: Spawn room layout as well (walls, etc)
        SpawnEnemies();
        GameObject enemyManager = new GameObject("EnemyManager");
        enemyManager.AddComponent<EnemyManager>();
        EnemyManager.Instance.numEnemies = enemiesForLayout[CurrentGame.CurrentRoom];
        EnemyManager.Instance.roomManager = this;
    }


    void SpawnEnemies()
    {
        string layoutFile = "Rooms/RoomLayout" + CurrentGame.CurrentRoom;
        var roomLayout = Resources.Load(layoutFile);
        GameObject layout = Instantiate(roomLayout, transform.position, Quaternion.identity) as GameObject;

        //TODO: To clean this up a bit we could define a RoomInfo behavior added to the
        // instantiaated room laout object

        Transform playerStart = layout.transform.Find("PlayerSpawn");
        Player.Instance.transform.position = playerStart.position;
    }

    void SpawnExitPortal(Vector2 spawnPos)
    {
        //Get random loc within room
        if(spawnPos == null)
        {
            spawnPos = new Vector2(
                Random.Range(0, roomSize.height),
                Random.Range(0, roomSize.height)
            );
        }

        // First time a portal spawns, spawn Blobber to tell you about it.
        if(CurrentGame.CurrentRoom == 0)
        {
            Vector2 blobberSpawn = spawnPos;
            float diff = 2.0f;
            // See if player is above or below the portal
            if (Player.Instance.transform.position.y < spawnPos.y)
            {
                Vector2 portalSpawn = new Vector2(spawnPos.x, spawnPos.y - (diff));
                Instantiate(exitPortal, portalSpawn, Quaternion.identity);
            }

            SpawnBlobberWithDialogueAt(blobberSpawn, new string[] { "aboutPortalsDialogue", "secondAboutPortalsDialogue" });
        } else
        {
            Instantiate(exitPortal, spawnPos, Quaternion.identity);
        }

    }

    public void OnRoomComplete(Vector2 spawnPortalPos)
    {
        Debug.Log("Room complete!");
        SpawnExitPortal(spawnPortalPos);
    }


    private void SpawnBlobberWithDialogueAt(Vector2 spawnPos, string[] dialogues)
    {

        GameObject blobber = Instantiate(GamePrefabs.Instance.Blobber, spawnPos, Quaternion.identity);
        blobber.GetComponentInChildren<DialogueTrigger>().dialogueIds = new List<string>(dialogues);
    }
}
