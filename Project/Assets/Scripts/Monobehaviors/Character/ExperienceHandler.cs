using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceHandler : MonoBehaviour
{
    [SerializeField] AudioSource expAudioSource;
    [SerializeField] AudioSource levelUpAudioSource;
    [SerializeField] Sound gatherExpSound;
    [SerializeField] Sound levelUpSound;

    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] TMP_Text experienceCountText;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] BarAnimation experienceBar;
    [SerializeField] int experienceCapInterval;
    [SerializeField] Damageable damageable;
    [SerializeField] int startingExpGoal;
    int toNextLevel;
    int level;
    int exp;

    void Start()
    {
        level = 1;
        toNextLevel = startingExpGoal;
        experienceBar.UpdateFill(exp, toNextLevel);
    }

    public void Add(int count)
    {
        exp += count;

        if (exp >= toNextLevel)
            LevelUp();
        else
            expAudioSource.PlayOneShot(gatherExpSound.RandomSound());

        experienceBar.UpdateFillAndFlash(exp, toNextLevel);
        experienceCountText.text = $"{exp}/{toNextLevel}";
    }

    void LevelUp()
    {
        exp = 0;
        toNextLevel += experienceCapInterval;
        damageable.Heal(damageable.MaxHealth);
        damageable.IncreaseMaxHealth(10);
        level++;
        currentLevelText.text = $"Lvl {level}";
        levelUpAudioSource.PlayOneShot(levelUpSound.RandomSound());
        StartCoroutine(EmitParticles());
    }

    IEnumerator EmitParticles()
    {
        particleSystem.Emit(25);
        yield return new WaitForSeconds(.1f);
        particleSystem.Emit(50);
        yield return new WaitForSeconds(.15f);
        particleSystem.Emit(100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Experience"))
        {
            Destroy(collision.gameObject);
            Add(2);
        }
    }
}
