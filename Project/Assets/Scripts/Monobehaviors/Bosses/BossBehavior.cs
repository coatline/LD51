using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] Boss bossData;

    [SerializeField] Damageable damageable;
    [SerializeField] SpriteRenderer sr;

    private void Start()
    {
        damageable.HealthChanged += Damaged;
        damageable.Died += Die;
    }

    void Die()
    {
        for (int i = 0; i < bossData.ExpOrbs; i++)
            PickupSpawner.I.SpawnExp(transform.position);
    }

    void Damaged(float current, float max)
    {
        if (current <= max / 3f)
            SwitchStages();
    }
}
