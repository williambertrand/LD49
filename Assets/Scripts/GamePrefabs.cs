using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePrefabs : MonoBehaviour
{
    #region Singleton
    public static GamePrefabs Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public GameObject SoulDrop;
    public GameObject PlayerSoulDrop;
    public GameObject Blobber;
}
