using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] TMP_Text countDownText;
    [SerializeField] SpriteRenderer preview;
    Stopwatch gameTimeStopwatch;
    Stopwatch stopwatch;
    public int BossesKilled { get; private set; }
    public
    bool gameOver;

    public string GetTimeSurvived => gameTimeStopwatch.Elapsed.TotalSeconds.ToString();
    public void StopSpawning() => gameOver = true;
    public void BossDied() => BossesKilled++;

    void Start()
    {
        gameTimeStopwatch = new();
        stopwatch = new();
        gameTimeStopwatch.Start();
        stopwatch.Start();
        StartCoroutine(SpawnBoss());
    }

    bool spawning;
    Boss nextBoss;

    private void Update()
    {
        if (gameOver) return;

        if (stopwatch.Elapsed.Seconds >= 10 && !spawning)
            StartCoroutine(SpawnBoss());

        countDownText.text = (10 - stopwatch.Elapsed.Seconds).ToString();

        if (stopwatch.Elapsed.Seconds >= 9)
            SetScale(3f);
        else if (stopwatch.Elapsed.Seconds >= 8)
            SetScale(2f);
        else if (stopwatch.Elapsed.Seconds >= 7)
            SetScale(1.5f);
    }

    IEnumerator SpawnBoss()
    {
        spawning = true;
        nextBoss = DataLibrary.I.EasyBosses.GetRandom();

        Vector2 position = new Vector3(Random.Range(-8, 8f), Random.Range(-1f, 5f));
        preview.transform.position = position;
        preview.sprite = nextBoss.Sprite;

        yield return new WaitForSeconds(1);

        Instantiate(nextBoss.Prefab, position, Quaternion.identity, transform);

        SetScale(1);
        spawning = false;
        preview.sprite = null;
        stopwatch.Restart();
    }

    void SetScale(float s) => countDownText.transform.localScale = Vector3.one * s;
}

