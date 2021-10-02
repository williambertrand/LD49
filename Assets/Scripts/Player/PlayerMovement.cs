using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class DIR
{
    public static int N = 0;
    public static int E = 1;
    public static int S = 2;
    public static int W = 3;

    public static Quaternion rotationForDir(int d)
    {
        return Quaternion.Euler(0, 0, -90 * d);
    }

}

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration;

    private Vector2 input;
    private Rigidbody2D rb;
    private Animator anim;

    public int currentDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //velocity = rb.velocity;
        //Vector2.MoveTowards(velocity, input, acceleration);
        rb.velocity = input * moveSpeed;

        if (input.x > 0)
        {
            currentDir = DIR.E;

        } else if (input.x < 0)
        {
            currentDir = DIR.W;
        } else if (input.y > 0)
        {
            currentDir = DIR.N;
        } else if (input.y < 0)
        {
            currentDir = DIR.S;
        }

        //anim.SetFloat("Movement", Mathf.Abs(input.sqrMagnitude));
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

}