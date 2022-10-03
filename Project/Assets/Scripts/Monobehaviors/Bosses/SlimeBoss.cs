using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : BossBehavior
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Jumper jumper;

    [SerializeField] float stage1JumpDuration;
    [SerializeField] float stage2JumpDuration;
    [SerializeField] float stage1JumpRate;
    [SerializeField] float stage2JumpRate;
    [SerializeField] float stage1Speed;
    [SerializeField] float stage2Speed;
    [SerializeField] Sprite[] stage1Sprites;
    [SerializeField] Sprite[] stage2Sprites;
    [SerializeField] SpriteRenderer sr;

    float movementSpeed;
    float jumpDuration;
    float moveChance;
    Sprite[] sprites;
    float jumpDelay;

    bool moving;
    bool jumping;
    int dir = 1;

    protected override int Stage
    {
        get
        {
            return base.Stage;
        }
        set
        {
            StopAllCoroutines();

            if (value == 1)
            {
                jumpDuration = stage1JumpDuration;
                movementSpeed = stage1Speed;
                jumpDelay = stage1JumpRate;
                sprites = stage1Sprites;
                moveChance = .15f;
            }
            else if (value == 2)
            {
                jumpDuration = stage2JumpDuration;
                movementSpeed = stage2Speed;
                jumpDelay = stage2JumpRate;
                sprites = stage2Sprites;
                moveChance = .5f;
            }

            StartCoroutine(WaitForNextJump());

            base.Stage = value;
        }
    }

    void FixedUpdate()
    {
        if (jumping)
        {
            jumper.PressJump();
            rb.velocity = new Vector2(movementSpeed * dir, rb.velocity.y);
        }
        else
        {
            if (moving)
                rb.velocity = new Vector2(movementSpeed * dir, rb.velocity.y);
            else
                rb.velocity = new Vector2(0, rb.velocity.y);
        }


        if (moving == false)
            if (waiting == false)
                if (jumping == false)
                    if (jumper.Grounded)
                        StartCoroutine(WaitForNextJump());
    }

    bool waiting;
    IEnumerator WaitForNextJump()
    {
        // Just landed

        waiting = true;
        sr.sprite = sprites[0];

        yield return new WaitForSeconds(jumpDelay);


        if (Random.Range(0, 1f) <= moveChance)
            StartCoroutine(Move());
        else
            StartCoroutine(HoldJump());

        waiting = false;
    }

    IEnumerator Move()
    {
        sr.sprite = sprites[1];
        moving = true;
        yield return new WaitForSeconds(1f);
        moving = false;
    }

    IEnumerator HoldJump()
    {
        if (Random.Range(0, 2) == 0)
        {
            if (transform.position.x < 0)
                dir = 1;
            else
                dir = -1;
        }

        jumping = true;

        yield return new WaitForSeconds(jumpDuration);

        jumping = false;
        jumper.ReleaseJumpButton();
        sr.sprite = sprites[1];
    }
}
