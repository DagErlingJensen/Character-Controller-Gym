using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode RunButton;

    public float MovementSpeed;
    public float RunSpeed;

    CharacterController controller;
    Vector3 movementDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movementDirection = (transform.right * x) + (transform.forward * z);

        if(Input.GetKey(RunButton))
        {
            Run();
        }
        else
        {
            Move();
        }
    }

    void Run()
    {
        Vector3 motion = movementDirection * RunSpeed * Time.deltaTime;
        controller.Move(motion);
    }

    void Move()
    {
        Vector3 motion = movementDirection * MovementSpeed * Time.deltaTime;
        controller.Move(motion);
    }
}
