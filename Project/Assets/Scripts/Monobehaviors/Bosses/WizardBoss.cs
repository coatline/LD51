using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : BossBehavior
{
    [SerializeField] float changeDirectionCutoff;
    [SerializeField] float acceleration;
    [SerializeField] float distance;
    [SerializeField] float maxSpeed;
    float startingX;

    void Awake()
    {
        startingX = transform.position.x;
        targetX = distance - changeDirectionCutoff;
        dir = 1;
    }

    float currentSpeed;
    float targetX;
    int intervals;
    int dir;

    void Update()
    {
        if (transform.position.x >= distance - changeDirectionCutoff)
        {
            intervals++;
            targetX = -distance + changeDirectionCutoff;
            dir = -1;
        }
        else if (transform.position.x < -distance + changeDirectionCutoff)
        {
            intervals++;
            targetX = distance - changeDirectionCutoff;
            dir = 1;
        }

        transform.Translate(new Vector3(Time.deltaTime * currentSpeed, 0));
        currentSpeed += dir * Time.deltaTime * Mathf.Lerp(transform.position.y, targetX, targetX - transform.position.x);
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        if (intervals == 4)
        {
            targetX = startingX;
        }

        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startingHeight + (hoverHeight * dir)), Time.deltaTime * currentSpeed);
    }
}
