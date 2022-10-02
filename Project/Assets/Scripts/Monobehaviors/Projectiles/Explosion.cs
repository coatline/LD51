using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Sound explosionSound;
    float damage;

    public void Setup(float damage)
    {
        this.damage = damage;
        audioSource.PlayOneShot(explosionSound.RandomSound());
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var d = collision.GetComponent<Damageable>();

        if (d != null)
            d.TakeDamage(damage);
    }
}
