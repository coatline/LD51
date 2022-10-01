using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    [SerializeField] UnityEvent StoppedMoving;
    [SerializeField] UnityEvent StartedMoving;

    [SerializeField] float defaultMovementSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Inputs inputs;
    float movementSpeed;
    bool moving;

    private void Awake()
    {
        movementSpeed = defaultMovementSpeed;
    }

    private void Update()
    {
        Move(inputs.XMovement);
    }

    public virtual void Move(float xInput)
    {
        if (xInput == 0)
            TryStopMoving();
        else
            MoveInputs(xInput);
    }

    void MoveInputs(float xInput)
    {
        if (moving == false)
        {
            StartedMoving?.Invoke();
            moving = true;
        }

        rb.velocity = new Vector2(xInput * movementSpeed, rb.velocity.y);
    }

    public void TryStopMoving()
    {
        if (rb.velocity.x != 0)
        {
            if (moving == true)
            {
                moving = false;
                StoppedMoving?.Invoke();
            }

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void SetMovementSpeedToDefault() => movementSpeed = defaultMovementSpeed;
    public void SetMovementSpeed(float speed) => movementSpeed = speed;
}
