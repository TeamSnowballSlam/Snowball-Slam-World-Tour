using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const int movementSpeed = 7; //Placeholder values. Will change after testing

    private const int slidingSpeed = 14; //Placeholder values. Will change after testing

    public int CurrentSpeed //Returns the movement speed, sliding speed, or 0 depending on the player's state
    {
        get
        {
            if (IsSliding)
            {
                //Sliding speed
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
    public bool IsMoving //Property to set the IsMoving bool and update the animator
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
    public bool IsSliding //Property to set the IsSliding bool and update the animator
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

    private Rigidbody rb; //The player's Rigidbody component
    private Animator animator; //The player's Animator component
    private Vector2 moveInput; //The player's vector 2 move input
    private Vector3 moveDirection; //Move input converted to a Vector3
    private const float UPDATEDELAY = 0.05f; //The delay between direction facing updates
    private float lastUpdate; //The time since the last direction update

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
        rb.velocity = new Vector3(moveDirection.x * CurrentSpeed, 0, moveDirection.z * CurrentSpeed); //Move the player based on the move direction and speed
        lastUpdate += Time.fixedDeltaTime;
        if (lastUpdate >= UPDATEDELAY //I have added a delay between changing the facing direction otherwise it is almost impossible to end up facing diagonally
            && moveDirection != transform.forward
            && moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
            lastUpdate = 0;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        IsMoving = moveInput != Vector2.zero; //If the move input is not zero then the player is moving
    }
}
