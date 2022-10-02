using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceHandler : MonoBehaviour
{
    public System.Action LeveledUp;

    [SerializeField] AudioSource expAudioSource;
    [SerializeField] AudioSource levelUpAudioSource;
    [SerializeField] Sound gatherExpSound;
    [SerializeField] Sound levelUpSound;

    [SerializeField] ParticleSystem levelUpParticles;
    [SerializeField] TMP_Text experienceCountText;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] BarAnimation experienceBar;
    [SerializeField] int experienceCapInterval;
    [SerializeField] Damageable damageable;
    [SerializeField] int startingExpGoal;
    public int Level => level;
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
        LeveledUp?.Invoke();
        StartCoroutine(EmitParticles());
    }

    IEnumerator EmitParticles()
    {
        levelUpParticles.Emit(25);
        yield return new WaitForSeconds(.1f);
        levelUpParticles.Emit(50);
        yield return new WaitForSeconds(.15f);
        levelUpParticles.Emit(100);
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
