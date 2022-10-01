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
    public event System.Action<Player> KilledBy;
    public event System.Action Respawned;
    [SerializeField] UnityEvent Healed;

    [SerializeField] float maxHealth;
    [SerializeField] SimpleFlash flash;
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
        if (Dead) return false;

        if (flash != null)
            flash.Flash();
        Health -= damage;
        Damaged?.Invoke(health);
        return Health < 0;
    }

    public void Kill(Player player)
    {
        Dead = true;
        KilledBy?.Invoke(player);
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
}
