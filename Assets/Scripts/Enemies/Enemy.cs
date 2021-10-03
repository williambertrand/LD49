using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum EnemyState
{
    IDLE,
    PATROL,
    CHASING,
    ATTACKING,
    DEAD
}

public class Enemy : MonoBehaviour
{

    public virtual void OnAttackedBy(Transform tar)
    {

    }
}
