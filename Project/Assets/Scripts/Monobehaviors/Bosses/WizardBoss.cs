using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBoss : BossBehavior
{
    [SerializeField] ContactDamager lightningPrefab;
    [SerializeField] Sprite[] stage1Sprites;
    [SerializeField] Sprite[] stage2Sprites;
    [SerializeField] SpriteRenderer sr;

    [Header("Movement")]
    [SerializeField] float changeDirectionCutoff;
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] float distance;
    [Header("Attack")]
    [SerializeField] float lightningDelay;
    [SerializeField] float chargeUpTime;
    Sprite[] sprites;
    float startingX;

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
                sprites = stage1Sprites;
            }
            else if (value == 2)
            {
                acceleration *= 2;
                sprites = stage2Sprites;
                lightningDelay /= 2;
                chargeUpTime /= 2;
            }

            StartCoroutine(SpawnLightning());

            base.Stage = value;
        }
    }

    void Awake()
    {
        startingX = transform.position.x;
        targetX = distance - changeDirectionCutoff;
        dir = 1;
    }

    void Update()
    {
        Hover();
    }

    float currentSpeed;
    float targetX;
    int intervals;
    int dir;

    void Hover()
    {
        if (transform.position.x >= distance - changeDirectionCutoff)
        {
            intervals++;
            targetX = -distance + changeDirectionCutoff;
            dir = -1;
        }
        else if (transform.position.x < -distance + changeDirectionCutoff)
        {
            intervals++;
            targetX = distance - changeDirectionCutoff;
            dir = 1;
        }

        transform.Translate(new Vector3(Time.deltaTime * currentSpeed, 0));
        currentSpeed += dir * Time.deltaTime * Mathf.Lerp(transform.position.y, targetX, targetX - transform.position.x) * acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        if (intervals == 4)
            targetX = startingX;
    }

    IEnumerator SpawnLightning()
    {
        sr.sprite = sprites[0];
        yield return new WaitForSeconds(lightningDelay - chargeUpTime);
        sr.sprite = sprites[1];
        yield return new WaitForSeconds(chargeUpTime);
        Instantiate(lightningPrefab, new Vector3(Random.Range(-8, 8f), Random.Range(-2f, 6f)), Quaternion.identity);
        StartCoroutine(SpawnLightning());
    }
}
