using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyBoss : BossBehavior
{
    [SerializeField] Bullet secondBullet;
    [SerializeField] Bullet firstBullet;
    [SerializeField] Sprite[] stage1Sprites;
    [SerializeField] Sprite[] stage2Sprites;
    [SerializeField] Sprite[] stage3Sprites;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Transform[] firingPoints;
    [SerializeField] Sound firstFireSound;
    [SerializeField] Sound secondFireSound;
    [SerializeField] string bulletLayer;

    [Header("Attack")]
    [SerializeField] float damage;
    [SerializeField] float rotateDelay;
    [SerializeField] float fireDelay;
    [SerializeField] float speed;
    [SerializeField] int bursts;
    float fireForce;

    Sound currentFireSound;
    Bullet currentBullet;
    Sprite[] sprites;

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
                currentFireSound = firstFireSound;
                currentBullet = firstBullet;
                sprites = stage1Sprites;
                fireForce = 2;
            }
            else if (value == 2)
            {
                sprites = stage2Sprites;
                rotateDelay /= 2;
                speed *= 2;
            }
            else if (value == 3)
            {
                currentBullet = secondBullet;
                sprites = stage3Sprites;
                rotateDelay /= 2;
                fireForce *= 2;
                speed *= 2;
                fireForce = 25;
            }

            StartStage();

            base.Stage = value;
        }
    }

    void StartStage()
    {
        targetPos = new Vector3(Random.Range(-10, 10f), Random.Range(-4f, 6f));
    }

    IEnumerator Fire()
    {
        sr.sprite = sprites[0];
        firing = true;
        for (int i = 0; i < bursts; i++)
        {
            sr.sprite = sprites[1];
            yield return new WaitForSeconds(fireDelay);

            foreach (Transform t in firingPoints)
            {
                Vector2 dir = (t.position - transform.position).normalized;
                Bullet b = Instantiate(currentBullet, t.position, Quaternion.identity);
                b.Setup(dir * fireForce, damage, currentFireSound, bulletLayer);
            }

            sr.sprite = sprites[0];
            targetRot += 90;
            yield return new WaitForSeconds(.5f);
        }

        firing = false;
        StartStage();
    }

    Vector3 targetPos;
    float targetRot;
    bool firing;

    void Update()
    {
        if (firing)
        {
            Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, targetRot), 1);
        }
        else
        {
            if (Vector2.Distance(transform.position, targetPos) < .25f)
            {
                StartCoroutine(Fire());
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
        }
    }
}
