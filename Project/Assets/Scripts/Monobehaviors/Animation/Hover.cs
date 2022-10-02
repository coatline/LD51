using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] float changeDirectionCutoff;
    [SerializeField] float hoverOffset;
    float startingHeight;
    float hoverHeight;

    void Start()
    {
        startingHeight = transform.position.y;
        hoverHeight = hoverOffset + startingHeight;
        dir = 1;
    }

    int dir;
    void Update()
    {
        if (dir == 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, hoverHeight, Time.deltaTime));

            if (hoverHeight - transform.position.y < changeDirectionCutoff)
                dir = -1;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, startingHeight, Time.deltaTime));

            if (transform.position.y - startingHeight < changeDirectionCutoff)
                dir = 1;
        }

    }
}
