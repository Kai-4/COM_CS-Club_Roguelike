using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveVector;
    Vector2 lookVector;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rollCooldown = 1.5f;
    private float rollCooldownTimer = 0f;

    private bool isRolling = false;

    Animator anim;
    Rigidbody2D rb;

    // move function using the unity input system. gets callback context whenever the move action is performed
    public void Move(InputAction.CallbackContext context)
    {
        if (!isRolling)
        {
            moveVector = context.ReadValue<Vector2>();
            rb.linearVelocity = moveVector * speed;
        }
        
    }

    // look function using the unity input system. gets callback context whenever the look action is performed
    public void Look(InputAction.CallbackContext context)
    {
        lookVector = context.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
