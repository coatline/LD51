using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] string[] groundLayersNames;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    LayerMask layerMask;

    [Header("Luxeries")]
    [SerializeField] float jumpPressEarlyTime;
    [SerializeField] float coyoteTime;
    [Header("Feel")]
    [SerializeField] float jumpingGravityCutoff;
    [SerializeField] float yVelFallingStart;
    [SerializeField] float fallingGravity;
    [SerializeField] float jumpingGravity;
    [Header("Input")]
    [SerializeField] float spacebarTime;
    [SerializeField] float jumpVelocity;
    [Header("Raycast")]
    [SerializeField] float raycastDepth;
    [SerializeField] float raycastOffsetY;

    int jumpingParameter;
    int fallingParameter;

    bool jumping;
    public bool Jumping
    {
        get { return jumping; }
        set
        {
            animator.SetBool(jumpingParameter, value);
            jumping = value;
        }
    }
    bool falling;
    public bool Falling
    {
        get { return falling; }
        set
        {
            animator.SetBool(fallingParameter, value);
            falling = value;
        }
    }

    public bool JumpLocked { get; set; }

    float lastOnGround;
    float lastHitJump;
    bool canBoost;
    bool grounded;

    private void Awake()
    {
        lastOnGround = -coyoteTime;
        lastHitJump = -jumpPressEarlyTime;

        jumpingParameter = Animator.StringToHash("Jumping");
        fallingParameter = Animator.StringToHash("Falling");
        layerMask = LayerMask.GetMask(groundLayersNames);
    }

    void Update()
    {
        if (!IsGrounded())
        {
            InAir();
        }
        else
        {
            OnGround();
        }
    }

    void OnGround()
    {
        lastOnGround = Time.time;

        grounded = true;

        if (rb.velocity.y > yVelFallingStart) return;

        Jumping = false;
        canBoost = true;

        if (!Falling) return;

        Falling = false;

        if (Time.time - lastHitJump < jumpPressEarlyTime)
        {
            lastHitJump = -jumpPressEarlyTime;
            print("sdf");
            TryJump();
        }
    }

    void InAir()
    {
        grounded = false;

        if (rb.velocity.y < jumpingGravityCutoff)
            rb.gravityScale = fallingGravity;

        if (rb.velocity.y > yVelFallingStart)
        {
            if (!jumping)
            {
                Falling = false;
                Jumping = true;
            }
        }
        else
        {
            Jumping = false;
            Falling = true;
        }
    }

    bool boosting;

    private void FixedUpdate()
    {
        if (boosting)
            Boost();

        void Boost()
        {
            rb.gravityScale = jumpingGravity;
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }

    public void ReleaseJumpButton()
    {
        canBoost = false;
        boosting = false;
        rb.gravityScale = fallingGravity;
    }

    void TryJump()
    {
        if (JumpLocked || canBoost == false) return;

        if (!grounded)
            // If we pressed the button within the amount of time allotted then continue even though we aren't on the ground
            if (Time.time - lastOnGround > coyoteTime)
                return;

        if (boosting == false)
            StartCoroutine(SpaceBar());

        boosting = true;
    }

    public void PressJump()
    {
        lastHitJump = Time.time;
        TryJump();
    }

    IEnumerator SpaceBar()
    {
        canBoost = true;
        yield return new WaitForSeconds(spacebarTime);
        boosting = false;
        canBoost = false;
    }

    bool IsGrounded()
    {
        Vector2 sizeOfSprite = new Vector2(sr.sprite.rect.size.x / sr.sprite.pixelsPerUnit, sr.sprite.rect.size.y / sr.sprite.pixelsPerUnit) * transform.localScale;
        // One for each leg (if it is a square shaped character
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position - new Vector3(sizeOfSprite.x / 2, (sizeOfSprite.y / 2) - raycastOffsetY), -transform.up, raycastDepth + raycastOffsetY, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position - new Vector3(-sizeOfSprite.x / 2, (sizeOfSprite.y / 2) - raycastOffsetY), -transform.up, raycastDepth + raycastOffsetY, layerMask);
        return (hit1 || hit2);
    }

    private void OnDrawGizmos()
    {
        Vector2 sizeOfSprite = new Vector2(sr.sprite.rect.size.x / sr.sprite.pixelsPerUnit, sr.sprite.rect.size.y / sr.sprite.pixelsPerUnit) * transform.localScale;
        Ray r = new Ray(transform.position - new Vector3(sizeOfSprite.x / 2, (sizeOfSprite.y / 2) - raycastOffsetY), -transform.up);
        Ray r2 = new Ray(transform.position - new Vector3(-sizeOfSprite.x / 2, (sizeOfSprite.y / 2) - raycastOffsetY), -transform.up);
        Gizmos.DrawRay(r);
        Gizmos.DrawRay(r2);
    }
}
