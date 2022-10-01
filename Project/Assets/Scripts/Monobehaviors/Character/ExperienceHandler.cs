using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] TMP_Text experienceCountText;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] BarAnimation experienceBar;
    [SerializeField] int experienceCapInterval;
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

        experienceBar.UpdateFillAndFlash(exp, toNextLevel);
        experienceCountText.text = $"{exp}/{toNextLevel}";
    }

    void LevelUp()
    {
        exp = 0;
        toNextLevel += experienceCapInterval;
        level++;
        currentLevelText.text = $"Lvl {level}";
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
