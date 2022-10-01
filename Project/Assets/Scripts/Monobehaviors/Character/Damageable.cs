using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public event System.Action<float, float> HealthChanged;
    /// <summary>
    /// float: Health remaining
    /// </summary>
    public event System.Action<float> Damaged;
    public event System.Action Respawned;
    public event System.Action Died;
    [SerializeField] UnityEvent Healed;
    [SerializeField] Animator animator;

    [SerializeField] float invincibilityTime;
    bool invincible;

    [SerializeField] float maxHealth;
    [SerializeField] SimpleFlash flash;
    [SerializeField] SpriteRenderer sr;
    public float MaxHealth => maxHealth;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            HealthChanged?.Invoke(health, maxHealth);
        }
    }
    float health;

    public bool Dead { get; private set; }

    /// <returns>Dead</returns>
    public bool TakeDamage(float damage)
    {
        if (Dead || invincible) return false;

        if (flash != null)
            flash.Flash();

        Health -= damage;
        Damaged?.Invoke(health);
        StartCoroutine(DoInvincibility());

        if (Health < 0)
            Kill();
        return Health < 0;
    }

    public void Kill()
    {
        Dead = true;
        Died?.Invoke();
        animator.Play("Die");
    }

    void Awake()
    {
        Respawn();
    }

    public void Respawn()
    {
        Dead = false;
        Health = maxHealth;
        Respawned?.Invoke();
    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(health + amount, maxHealth);
        print($"Healed: {amount} Health: {Health}");
    }

    IEnumerator DoInvincibility()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

    public void Destroy() => Destroy(gameObject);
}


//[SerializeField] bool showInvincibility;
//[SerializeField] float invincibilityFlashSpeed;
//[SerializeField] float invincibilityTime;
//bool invincible;


//IEnumerator DoInvincibility()
//{
//    invincible = true;
//    yield return new WaitForSeconds(invincibilityTime);
//    invincible = false;
//}

//int alphaDir;

//private void Update()
//{
//    if (sr.color.a <= 0)
//        alphaDir = 1;
//    else if (sr.color.a >= 1)
//        alphaDir = -1;

//    if (invincible)
//        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + (Time.deltaTime * alphaDir));
//}