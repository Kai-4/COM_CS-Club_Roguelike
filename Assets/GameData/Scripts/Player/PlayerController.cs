using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveVector;
    Vector2 lookVector;
    Vector2 rollDir;

    [SerializeField]
    private float speed;
    [SerializeField]

    private float rollCooldown = 1.5f;
    private float rollSpeedMult = 1.6f;

    private bool isRolling = false;
    private bool canRoll = true;

    Animator anim;
    Rigidbody2D rb;

    // move function using the unity input system. gets callback context whenever the move action is performed
    public void Move(InputAction.CallbackContext context)
    {
        
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
        moveVector = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isRolling)
        {
            moveVector.x = Input.GetAxisRaw("Horizontal");
            moveVector.y = Input.GetAxisRaw("Vertical");
            moveVector.Normalize();
        }
        rb.linearVelocity = moveVector * speed * (isRolling ? rollSpeedMult : 1);
        anim.SetFloat("Vel", moveVector.magnitude);
    }

    IEnumerator RollCooldownStart()
    {
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

    public void StartRoll()
    {
        if (canRoll)
        {
            isRolling = true;
            canRoll = false;
            if (moveVector.x >= 0)
            {
                anim.Play("RollRight");
            }
            else
            {
                anim.Play("RollLeft");
            }
        }
    }

    public void EndRoll()
    {
        isRolling = false;
        rollSpeedMult = 1.6f;
    StartCoroutine(RollCooldownStart());
    }

    public void setRollSpeedMult(float rsm)
    {
        rollSpeedMult = rsm;
    }
}
