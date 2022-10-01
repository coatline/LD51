using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    [SerializeField] Damageable health;

    [Header("Static")]
    [SerializeField] BarAnimation healthBar;

    [Header("Created During Runtime")]
    [SerializeField] BarAnimation healthBarPrefab;
    [SerializeField] Transform position;

    [Header("Optional")]
    [SerializeField] TMP_Text healthCountText;


    void Awake()
    {
        health.HealthChanged += HealthChanged;

        if (healthBar == null)
        {
            Canvas[] canvasas = FindObjectsOfType<Canvas>();
            Canvas c = null;

            foreach (Canvas canvas in canvasas)
            {
                if (canvas.name == "WorldSpaceCanvas")
                {
                    c = canvas;
                    break;
                }
            }

            healthBar = Instantiate(healthBarPrefab, c.transform);
        }
    }

    void HealthChanged(float current, float max)
    {
        if (healthCountText != null)
            healthCountText.text = $"{current}/{max}";
        healthBar.UpdateFillAndFlash(current, max);
    }

    private void Update()
    {
        if (position)
            healthBar.transform.position = position.position;
    }

    private void OnDestroy()
    {
        // If we created ours, destroy it too
        if (healthBarPrefab != null)
            Destroy(healthBar.gameObject);

        health.HealthChanged -= HealthChanged;
    }
}
