using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveVector;
    Vector2 lookVector;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField]
    private float speed;
    // move function using the unity input system. gets callback context whenever the move action is performed
    public void Move(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
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
        rb.linearVelocity = moveVector * speed;
        if (moveVector.magnitude != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
