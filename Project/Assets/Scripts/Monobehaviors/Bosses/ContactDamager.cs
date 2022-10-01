using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    Dictionary<Damageable, Coroutine> damageables;
    [SerializeField] List<string> ignoreTags;
    [SerializeField] float damagePerTick;
    [SerializeField] float tickInterval;

    void Start()
    {
        damageables = new();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ignoreTags.Contains(collision.gameObject.tag)) return;
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            if (damageables.ContainsKey(damageable) == false)
                damageables.Add(damageable, StartCoroutine(Damage(damageable)));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ignoreTags.Contains(collision.gameObject.tag)) return;
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
            if (damageables.ContainsKey(damageable))
            {
                StopCoroutine(damageables[damageable]);
                damageables.Remove(damageable);
            }
    }

    IEnumerator Damage(Damageable d)
    {
        while (true)
        {
            d.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }
    }
}
