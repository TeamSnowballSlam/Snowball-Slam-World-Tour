using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] //To change in inspector
    private float movementSpeed = 3f;

    [SerializeField] //To change in inspector
    private float slidingSpeed = 5f;

    public float CurrentSpeed
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
    private Vector3 moveInput;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveInput.x * CurrentSpeed, moveInput.y * CurrentSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
