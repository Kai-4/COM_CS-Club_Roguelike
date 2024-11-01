using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTownController : MonoBehaviour
{
    private float speed = 10f;
    Vector2 moveVector = new Vector2(0,0);
    // move function using the unity input system. gets callback context whenever the move action is performed
    public void Move(InputAction.CallbackContext context)
    {
       
        moveVector.x = context.ReadValue<Vector2>().x;
        Debug.Log(moveVector.x);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveVector * speed * Time.deltaTime);
    }
}
