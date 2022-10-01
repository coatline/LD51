using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] TMP_Text countDownText;
    [SerializeField] SpriteRenderer preview;
    Stopwatch stopwatch;

    void Start()
    {
        stopwatch = new();
        stopwatch.Start();
        SpawnBoss();
    }

    Boss nextBoss;

    private void Update()
    {
        if (stopwatch.Elapsed.Seconds >= 10)
            SpawnBoss();

        countDownText.text = (10 - stopwatch.Elapsed.Seconds).ToString();

        if (stopwatch.Elapsed.Seconds >= 9)
            SetScale(4);
        else if (stopwatch.Elapsed.Seconds >= 8)
            SetScale(3f);
        else if (stopwatch.Elapsed.Seconds >= 7)
            SetScale(2);
    }

    void SpawnBoss()
    {
        Boss b = DataLibrary.I.EasyBosses.GetRandom();
        Instantiate(b.Prefab, transform);
        countDownText.transform.localScale = Vector3.one;
        stopwatch.Restart();
        SetScale(1);
    }
    void SetScale(float s) => countDownText.transform.localScale = Vector3.one * s;
}
