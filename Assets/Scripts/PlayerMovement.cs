using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Public variables")]
    public float MovementSpeed = 10;
    public float GravityMultiplier = 1;
    public float JumpHeight = 3;

    public Transform GroundCheck;
    public LayerMask GroundLayer;

    [Header("Private variables")]
    float gravityConst = -9.81f;
    public bool isGrounded;
    public bool CanDoubleJump;
    public Vector3 velocity;

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
            if (!CanDoubleJump)
                CanDoubleJump = true;
        }
        else
            velocity.y += gravityConst * GravityMultiplier * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.right * x + transform.forward * z;
        Vector3 movement = movementDirection * MovementSpeed * Time.deltaTime;
        controller.Move(movement);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravityConst * GravityMultiplier);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && CanDoubleJump)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravityConst * GravityMultiplier);
            CanDoubleJump = false;
        }

        //velocity.y += gravityConst * GravityMultiplier * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
