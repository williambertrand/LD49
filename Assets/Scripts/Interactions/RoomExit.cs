using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RoomExitType
{
    Scene,
    Room,
    Blobber,
    Restart,
    Ending,
    Credits
}

public class RoomExit: Interactable
{
    public RoomExitType exitType;

    public override void OnInteract()
    {
        if(exitType == RoomExitType.Scene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        RoomManager.Instance.LoadNextRoom(exitType);
    }
}
