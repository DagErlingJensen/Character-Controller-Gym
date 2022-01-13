using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Public variables")]
    public float MovementSpeed = 10;
    public float RunSpeed = 20;
    public float CrouchSpeed = 1;
    public float GravityMultiplier = 1;
    public float JumpHeight = 3;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    [Header("Private variables")]
    float gravityConst = -9.81f;
    public bool isGrounded;
    public bool canDoubleJump;
    public Vector3 velocity;
    public Vector3 movementDirection;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, .4f, GroundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
            if (!canDoubleJump)
                canDoubleJump = true;
        }
        else
            velocity.y += gravityConst * GravityMultiplier * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movementDirection = transform.right * x + transform.forward * z;
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            Run();
        }
        else if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            Crouch();
        }
        else
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 0.6f, 1);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, 1f, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }

        //velocity.y += gravityConst * GravityMultiplier * Time.deltaTime;
    }

    void Move()
    {
        Vector3 movement = movementDirection * MovementSpeed * Time.deltaTime;
        controller.Move(movement);
    }

    void Run()
    {
        Vector3 movement = movementDirection * RunSpeed * Time.deltaTime;
        controller.Move(movement);
    }

    void Crouch()
    {
        Vector3 movement = movementDirection * CrouchSpeed * Time.deltaTime;
        controller.Move(movement);
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravityConst * GravityMultiplier);
    }
}
