using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    [SerializeField] TMP_Text healthCountText;
    [SerializeField] BarAnimation healthBar;
    [SerializeField] Damageable health;

    void Awake()
    {
        health.HealthChanged += HealthChanged;
    }

    void HealthChanged(float current, float max)
    {
        healthCountText.text = $"{current}/{max}";
        healthBar.UpdateFillAndFlash(current, max);
    }

    private void OnDestroy()
    {
        health.HealthChanged -= HealthChanged;
    }
}
