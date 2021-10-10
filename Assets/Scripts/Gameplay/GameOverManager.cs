using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{

    [SerializeField] TMP_Text roomsClearedText;

    // Start is called before the first frame update
    void Start()
    {
        roomsClearedText.text = "Rooms cleared: " + (CurrentGame.CurrentRoom - 1) + " of " + CurrentGame.TotalRooms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
