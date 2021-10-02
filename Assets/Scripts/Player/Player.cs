using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    // References to player behaviors
    public SoulStability stability;

    // Start is called before the first frame update
    void Start()
    {
        stability = GetComponent<SoulStability>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
