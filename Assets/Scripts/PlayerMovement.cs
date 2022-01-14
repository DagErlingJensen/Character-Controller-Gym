using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Public variables")]
    public float MovementSpeed = 10;
    public float RunSpeed = 15;
    public float CrouchSpeed = 1;
    public float GravityMultiplier = 1;
    public float JumpHeight = 2;
    public float LongJumpHeight = 3.5f;
    public float LongJumpTime = 0.08f;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    [Header("Private variables")]
    float gravityConst = -9.81f;
    public bool isGrounded;
    public bool canDoubleJump;
    public bool isJumping;
    public Vector3 velocity;
    public Vector3 movementDirection;
    float longJumpTimer;

    float jumpYPos;

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

            if(isJumping)
                isJumping = false;

            if (!canDoubleJump)
                canDoubleJump = true;
        }
        else
            velocity.y += gravityConst * GravityMultiplier * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movementDirection = transform.right * x + transform.forward * z;
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }

        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            longJumpTimer += Time.deltaTime;
            if(longJumpTimer > LongJumpTime)
            {
                LongJump();
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            Crouch();
        }
        else if(Input.GetKey(KeyCode.CapsLock) && isGrounded)
        {
            Run();
        }
        else
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height *= 0.5f;
            Camera.main.transform.localPosition = new Vector3(0, 0.75f, 0.5f);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height *= 2;
            Camera.main.transform.localPosition = new Vector3(0, 1.25f, 0.5f);
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
        longJumpTimer = 0;
        isJumping = true;
        jumpYPos = transform.position.y;
        velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravityConst * GravityMultiplier);
    }

    void LongJump()
    {
        isJumping = false;
        longJumpTimer = 0;
        velocity.y = 0;
        float heightDiff = transform.position.y - jumpYPos;
        velocity.y = Mathf.Sqrt((LongJumpHeight - heightDiff) * -2 * gravityConst * GravityMultiplier);
    }
}
