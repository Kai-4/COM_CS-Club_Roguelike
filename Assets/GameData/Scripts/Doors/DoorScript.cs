using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D myCollider;
    [SerializeField]
    private BoolVariable isLocked;
    private bool isOpen = false;

    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Locked", isLocked.value);
        if (isLocked.value && isOpen)
        {
            Close();
        }
        anim.SetBool("Open", isOpen);
    }

    private void Open()
    {
        if (!isLocked.value)
        {
            isOpen = true;
            myCollider.enabled = false;
        }
    }

    private void Close()
    {
        isOpen = false;
        myCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Open();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Close();
    }
}
