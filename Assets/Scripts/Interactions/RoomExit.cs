using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RoomExitType
{
    Scene,
    Room
}

public class RoomExit: Interactable
{
    public RoomExitType exitType;

    public override void OnInteract()
    {
        if(exitType == RoomExitType.Scene)
        {
            // Load next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(exitType == RoomExitType.Room)
        {
            // Just re-load the current scene
            CurrentGame.CurrentRoom += 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
