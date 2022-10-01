using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    [SerializeField] Damageable damageable;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Jumper jumper;

    [SerializeField] float stage1Speed;
    [SerializeField] float stage2Speed;
    [SerializeField] Sprite[] stage1Sprites;
    [SerializeField] Sprite[] stage2Sprites;
    bool jumping;
    float speed;
    int dir = 1;
    bool stage2;

    private void Start()
    {
        speed = stage1Speed;
        StartCoroutine(WaitForNextJump());
        damageable.HealthChanged += Damaged;
    }

    void Damaged(float current, float max)
    {
        if (current <= max / 3f && !stage2)
            SwitchStages();
    }

    void FixedUpdate()
    {
        if (jumping)
        {
            jumper.PressJump();
            rb.velocity = new Vector2(speed * dir, rb.velocity.y);
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

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

        if (stage2)
        {

            yield return new WaitForSeconds(GetJumpDelay() / 2f);
            StartCoroutine(HoldJumpStage2());
        }
        else
        {

            yield return new WaitForSeconds(GetJumpDelay());
            StartCoroutine(HoldJumpStage1());
        }
        waiting = false;
    }

    Coroutine coroutine;

    IEnumerator HoldJumpStage1()
    {
        sr.sprite = stage1Sprites[0];

        if (transform.position.x < 0)
            dir = 1;
        else
            dir = -1;

        jumping = true;

        yield return new WaitForSeconds(GetJumpTime());

        jumping = false;
        jumper.ReleaseJumpButton();
        sr.sprite = stage1Sprites[1];
    }

    IEnumerator HoldJumpStage2()
    {
        sr.sprite = stage2Sprites[0];

        if (Random.Range(0, 2) == 0)
        {
            if (transform.position.x < 0)
                dir = 1;
            else
                dir = -1;
        }

        jumping = true;

        yield return new WaitForSeconds(GetJumpTime());

        jumping = false;
        jumper.ReleaseJumpButton();
        sr.sprite = stage2Sprites[1];
    }

    void SwitchStages()
    {
        stage2 = true;
        speed = stage2Speed;
    }

    float GetJumpTime()
    {
        if (dir == 1)
            return .5f;
        else
            return 1f;
    }

    float GetJumpDelay()
    {
        if (dir == 1)
            return 1f;
        else
            return .5f;
    }
}
