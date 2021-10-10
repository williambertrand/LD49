using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Size
{
    public int width;
    public int height;
}

public enum RoomType
{
    Combat,
    Dialogue
}

public class LayoutInfo
{
    public int enemies;
    public RoomType roomType;

    public LayoutInfo(int e, RoomType t)
    {
        this.enemies = e;
        this.roomType = t;
    }
}

public class RoomManager : MonoBehaviour
{

    public static RoomManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] private Size roomSize;
    [SerializeField] private GameObject exitPortal;
    [SerializeField] private Animator sceneTransition;
    [SerializeField] private bool spawnLayout;

    private LayoutInfo[] enemiesForLayout;

    // Start is called before the first frame update
    void Start()
    {
        enemiesForLayout = new LayoutInfo[] {
            new LayoutInfo(3, RoomType.Combat),
            new LayoutInfo(2, RoomType.Combat),
            new LayoutInfo(4, RoomType.Combat),
            new LayoutInfo(5, RoomType.Combat),
            new LayoutInfo(0, RoomType.Dialogue),
            new LayoutInfo(7, RoomType.Combat),
            new LayoutInfo(11, RoomType.Combat),
            new LayoutInfo(5, RoomType.Combat),
            new LayoutInfo(1, RoomType.Combat), // Big spinner
            //new LayoutInfo(10, RoomType.Combat),
        };

        // Todo if time: Spawn room layout as well (walls, etc)
        if(spawnLayout)
        {
            SpawnEnemies();
            GameObject enemyManager = new GameObject("EnemyManager");
            enemyManager.AddComponent<EnemyManager>();
            //EnemyManager.Instance.numEnemies = enemiesForLayout[CurrentGame.CurrentRoom].enemies;
            EnemyManager.Instance.roomManager = this;
        }
    }


    void SpawnEnemies()
    {
        if(enemiesForLayout[CurrentGame.CurrentRoom].roomType == RoomType.Combat)
        {
            string layoutFile = "Rooms/RoomLayout" + CurrentGame.CurrentRoom;
            var roomLayout = Resources.Load(layoutFile);
            GameObject layout = Instantiate(roomLayout, transform.position, Quaternion.identity) as GameObject;
            Transform playerStart = layout.transform.Find("PlayerSpawn");
            Player.Instance.transform.position = playerStart.position;
        }
       
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
            float diff = 2.5f;
            // See if player is above or below the portal
            if (Player.Instance.transform.position.y < spawnPos.y)
            {
                blobberSpawn = new Vector2(spawnPos.x, spawnPos.y - (diff));
                Instantiate(exitPortal, spawnPos, Quaternion.identity);
            } else
            {
                blobberSpawn = new Vector2(spawnPos.x, spawnPos.y + (diff));
                Instantiate(exitPortal, spawnPos, Quaternion.identity);
            }

            SpawnBlobberWithDialogueAt(blobberSpawn, new string[] { "aboutPortalsDialogue", "secondAboutPortalsDialogue" });
        } else
        {
            GameObject portal = Instantiate(exitPortal, spawnPos, Quaternion.identity);
            if(CurrentGame.CurrentRoom == 3)
            {
                portal.GetComponent<RoomExit>().exitType = RoomExitType.Blobber;
            }
            else if (CurrentGame.CurrentRoom == enemiesForLayout.Length - 1)
            {
                portal.GetComponent<RoomExit>().exitType = RoomExitType.Ending;
            }
        }

    }

    public void OnRoomComplete(Vector2 spawnPortalPos)
    {
        SpawnExitPortal(spawnPortalPos);
    }


    private void SpawnBlobberWithDialogueAt(Vector2 spawnPos, string[] dialogues)
    {
        // Patch for bug where we may spawn multiple portals
        if(Blobber.Instance != null)
        {
            return;
        }
        GameObject blobber = Instantiate(GamePrefabs.Instance.Blobber, spawnPos, Quaternion.identity);
        blobber.GetComponentInChildren<DialogueTrigger>().dialogueIds = new List<string>(dialogues);
    }

    public void OnPlayerDeath()
    {
        // Go to gameover scene
        SceneManager.LoadScene(GameScenes.DEATH);
    }


    public void LoadNextRoom(RoomExitType exitType)
    {
        StartCoroutine(LoadNextRoomScene(exitType));
    }

    IEnumerator LoadNextRoomScene(RoomExitType exitType)
    {
        sceneTransition.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1.0f);

        if (exitType == RoomExitType.Scene)
        {
            // Load next scene - used on tutorial
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (exitType == RoomExitType.Room)
        {
            // Just re-load the current scene after incrementing current room
            CurrentGame.CurrentRoom += 1;
            CurrentGame.Stability = Player.Instance.stability.current;
            SceneManager.LoadScene(GameScenes.GAMEPLAY);
        }
        else if (exitType == RoomExitType.Blobber)
        {
            CurrentGame.CurrentRoom += 1;
            // Just re-load the current scene after incrementing current room
            SceneManager.LoadScene(GameScenes.STORE);
        }
        else if (exitType == RoomExitType.Ending)
        {
            // Just re-load the current scene after incrementing current room
            SceneManager.LoadScene(GameScenes.END);
        }
        else if (exitType == RoomExitType.Credits)
        {
            // Just re-load the current scene after incrementing current room
            SceneManager.LoadScene(GameScenes.Credits);
        }
        else if (exitType == RoomExitType.Restart)
        {
            // Reset current room and start
            CurrentGame.CurrentRoom = 0;
            SceneManager.LoadScene(GameScenes.GAMEPLAY);
        }
    }

    public void LoadCredits()
    {
        StartCoroutine(LoadNextRoomScene(RoomExitType.Credits));
    }

}
