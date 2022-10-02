using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] float changeDirectionCutoff;
    [SerializeField] float hoverHeight;
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    float startingHeight;

    void Start()
    {
        startingHeight = transform.position.y;
        targetHeight = startingHeight + hoverHeight - changeDirectionCutoff;
        dir = 1;
    }

    float currentSpeed;
    float targetHeight;
    int dir;
    void Update()
    {
        if (transform.position.y >= startingHeight - changeDirectionCutoff)
        {
            targetHeight = startingHeight + changeDirectionCutoff;
            dir = 1;
        }
        else if (transform.position.y < startingHeight + changeDirectionCutoff)
        {
            targetHeight = startingHeight + hoverHeight - changeDirectionCutoff;
            dir = -1;
        }

        transform.Translate(new Vector3(0, Time.deltaTime * currentSpeed, 0));
        currentSpeed += dir * Time.deltaTime * Mathf.Lerp(transform.position.y, targetHeight, targetHeight- transform.position.y);
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startingHeight + (hoverHeight * dir)), Time.deltaTime * currentSpeed);
    }
}
