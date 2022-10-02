using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Play shooting sound on projectiles so that fast firing weapons do not cut off the sounds

    [SerializeField] protected ParticleSystem particles;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] AudioSource audioSource;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D col;
    //[SerializeField] Sound soundOnHit;

    [SerializeField] Color playerStartColor;
    [SerializeField] int durability;

    protected float knockback;
    protected Player player;
    protected float damage;
    //Sound soundOnHitWall;
    Vector2 linearDrag;
    float minimumVel;
    bool dead;

    public void Setup(Vector3 direction, Weapon weapon, Player player, Collider2D[] ignoreColliders = null)
    {
        rb.velocity = direction * weapon.AttackForce;

        rb.gravityScale = weapon.AttackGravity;
        linearDrag = weapon.AttackLinearDrag;
        minimumVel = weapon.FadeOutMagnitude;
        knockback = weapon.Knockback;
        damage = weapon.Damage;
        this.player = player;

        // So that the physics system does not determine knockback.
        rb.mass = 0.000001f;

        audioSource.PlayOneShot(weapon.SoundOnUse.RandomSound());
        StartCoroutine(LifeTime(weapon.MaxLifeTime));
        IgnoreColliders(ignoreColliders);
    }

    public void Setup(Vector3 force, float damage, Sound sound, string layer)
    {
        rb.velocity = force;

        this.damage = damage;

        // So that the physics system does not determine knockback.
        rb.mass = 0.000001f;

        gameObject.layer = LayerMask.NameToLayer(layer);
        audioSource.PlayOneShot(sound.RandomSound());
    }

    //public void Setup(Vector3 force, float gravity, Vector2 linearDrag, float damage, float knockback, Collider2D[] ignoreColliders = null /*Sound soundOnShot*/)
    //{
    //    rb.gravityScale = gravity;
    //    rb.velocity = force;

    //    this.linearDrag = linearDrag;
    //    this.damage = damage;
    //    this.knockback = knockback;

    //    // So that the physics system does not determine knockback.
    //    rb.mass = 0.0001f;

    //    IgnoreColliders(ignoreColliders);
    //    //audioSource.PlayOneShot(soundOnShot.GetClip());
    //}

    void IgnoreColliders(Collider2D[] cols)
    {
        if (col != null)
            foreach (Collider2D collider in cols)
                Physics2D.IgnoreCollision(col, collider);
    }

    IEnumerator LifeTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject != null)
            DestroyProjectile(true);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < minimumVel)
        {
            // Play fade out animation
            DestroyProjectile(true);
        }

        rb.velocity = rb.velocity / (linearDrag + Vector2.one);
    }

    public virtual void DestroyProjectile(bool fadeOut)
    {
        if (dead) { return; }
        dead = true;

        if (animator)
        {
            if (fadeOut)
                animator.Play("Fade_Out");
            else
                animator.Play("Bullet_Explode");

            //ads.PlayOneShot(soundOnHit.sound.GetClip());
        }
        else
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

    protected void Hit()
    {
        //if (soundOnHit)
        //    audioSource.PlayOneShot(soundOnHit.sound.GetClip());

        durability--;

        if (durability <= 0)
        {
            // Play explode animation
            DestroyProjectile(false);
        }
    }

    protected void Hit(Collision2D hit)
    {
        Damageable damageable = hit.gameObject.GetComponent<Damageable>();

        if (damageable.TakeDamage(damage))
        {
            damageable.Kill();
        }

        Hit();
    }
}