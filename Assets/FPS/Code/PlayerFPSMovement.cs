using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPSMovement : MonoBehaviour
{
    [Header("Public variables")]
    public float MovementSpeed = 10;
    public float RunSpeed = 20;
    public float JumpHeight = 2;
    public float GravityMultiplier = 2;
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    [Header("Private variables")]
    private CharacterController controller;
    private Vector3 movementDirection;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector3 velocity; //+
    private static float gravityConst = -9.81f; //+


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //MOVEMENT
        //Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Calculating movement direction
        movementDirection = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.LeftShift))
            Run();
        else
            Move();

        //isGrounded = Physics.CheckSphere(GroundCheck.position, GroundCheck.localScale.x / 2, GroundLayer);
        isGrounded = Physics.CheckBox(GroundCheck.position, GroundCheck.localScale, Quaternion.identity, GroundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5;
        }
        else
        {
            velocity.y += gravityConst * GravityMultiplier * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();
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

    void Jump()
    {
        velocity.y = Mathf.Sqrt(JumpHeight * -2 * gravityConst * GravityMultiplier);
    }
}
