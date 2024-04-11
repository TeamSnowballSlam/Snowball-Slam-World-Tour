using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const int movementSpeed = 7;

    private const int slidingSpeed = 14;

    public int CurrentSpeed
    {
        get
        {
            if (IsSliding)
            {
                //Change to sliding speed when implemented
                return slidingSpeed;
            }
            if (IsMoving)
            {
                //Moving Speed
                return movementSpeed;
            }
            else
            {
                //Idle Speed
                return 0;
            }
        }
    }

    private bool isMoving = false;
    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        private set
        {
            isMoving = value;
            animator.SetBool("IsMoving", value);
        }
    }
    
    private bool isSliding = false;
    public bool IsSliding
    {
        get
        {
            return isSliding;
        }
        private set
        {
            isSliding = value;
            animator.SetBool("IsSliding", value);
        }
    }

    private Rigidbody rb;
    private Animator animator;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogWarning("No Rigidbody component found.");
        }
        if (animator == null)
        {
            Debug.LogWarning("No Animator component found.");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveInput.x * CurrentSpeed, 0, moveInput.y * CurrentSpeed);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero; //If the move input is not zero then the player is moving
        
        //Change direction of the player
    }
}
