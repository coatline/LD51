using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBehavior : MonoBehaviour
{
    [SerializeField] BarAnimation reloadBarPrefab;
    [SerializeField] Transform position;

    public bool AutoReloading { get; private set; }
    public bool Reloading { get; private set; }
    GunStack gunStack;
    BarAnimation bar;

    void Awake()
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

        bar = Instantiate(reloadBarPrefab, c.transform);
    }

    public void Setup(GunStack stack)
    {
        gunStack = stack;
        gunStack.Shot += Shot;
        gunStack.ShotsGone += AutoReload;
        interval = gunStack.GunType.ReloadTime / gunStack.MaxShots;
    }

    void Shot() => bar.UpdateFillAndFlash(gunStack.ShotsRemaining, gunStack.MaxShots);

    public void ToggleReload()
    {
        if (AutoReloading) return;

        if (Reloading == false)
            StartReloading();
        else
            StopReloading();
    }

    public void FullReload()
    {
        gunStack.FullReload();
        Reloading = false;
        AutoReloading = false;
    }

    void AutoReload()
    {
        StartReloading();
        AutoReloading = true;
        bar.SetColor(Color.blue);
    }

    public void StartReloading()
    {
        if (gunStack.FullyReloaded) return;
        Reloading = true;
    }

    public void StopReloading()
    {
        if (AutoReloading) return;
        Reloading = false;
    }

    float interval;
    float timer;
    void Update()
    {
        bar.transform.position = position.position;

        if (Reloading)
        {
            timer += Time.deltaTime;

            if (AutoReloading)
                bar.UpdateFill((timer + gunStack.ShotsRemaining), gunStack.MaxShots);

            if (timer > interval)
            {
                gunStack.Reload();
                timer = 0;

                if (AutoReloading == false)
                    bar.UpdateFillAndFlash(gunStack.ShotsRemaining, gunStack.MaxShots);

                if (gunStack.FullyReloaded)
                {
                    if (AutoReloading)
                        bar.SetDefaultColor();

                    Reloading = false;
                    AutoReloading = false;
                    return;
                }
            }
        }
    }

    private void OnDestroy()
    {
        gunStack.ShotsGone -= StartReloading;
        gunStack.Shot -= Shot;
    }
}
