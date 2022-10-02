using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Damageable playerDamageable;
    [SerializeField] TMP_Text bossesKilledText;
    [SerializeField] TMP_Text timeSurvivedText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] BossSpawner spawner;
    [SerializeField] GameObject screen;
    int level;
    public void SetLevel(int val) => level = val;

    void Start()
    {
        playerDamageable.Died += Died;
    }

    void Died()
    {
        spawner.StopSpawning();
        bossesKilledText.text = $"Killed: {spawner.BossesKilled}";
        timeSurvivedText.text = $"Survived: {spawner.GetTimeSurvived}";
        levelText.text = $"Level: {playerDamageable.GetComponent<ExperienceHandler>().Level}";
        screen.SetActive(true);
    }

}
