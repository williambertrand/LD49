using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blobber : MonoBehaviour
{
    public static Blobber Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
