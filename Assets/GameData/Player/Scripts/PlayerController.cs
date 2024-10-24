using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveVector;
    Vector2 lookVector;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
