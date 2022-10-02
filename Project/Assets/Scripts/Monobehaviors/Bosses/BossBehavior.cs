using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] Boss bossData;

    [SerializeField] Damageable damageable;
    [SerializeField] int stages;

    // Start at stage 1
    int stage;
    protected virtual int Stage { get { return stage; } set { stage = value; } }

    private void Start()
    {
        damageable.HealthChanged += Damaged;
        damageable.Died += Die;
        Stage = 1;
    }

    void Die()
    {
        for (int i = 0; i < bossData.ExpOrbs; i++)
            PickupSpawner.I.SpawnExp(transform.position);

        GetComponentInParent<BossSpawner>().BossDied();
    }

    void Damaged(float current, float max)
    {
        float percentage = current / max;
        //print($"{1 - percentage}, {(1 - percentage) * stages}, {(1 - percentage) * (stages - Stage)}, {(1 - percentage) * (Stage - stages)}");
        int targetStage = Mathf.CeilToInt((1 - percentage) * stages);

        if (Stage != targetStage)
            Stage = targetStage;
    }
}
