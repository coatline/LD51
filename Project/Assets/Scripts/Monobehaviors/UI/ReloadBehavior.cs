using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBehavior : MonoBehaviour
{
    [SerializeField] BarAnimation reloadBarPrefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Sound reloadingSound;
    [SerializeField] Sound finishedReloadingSound;
    [SerializeField] Color reloadingColor;
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
        if (gunStack != null)
        {
            gunStack.Shot -= Shot;
            gunStack.ShotsGone -= AutoReload;
            AutoReloading = false;
            Reloading = false;
        }

        reloadTimer = 0;
        gunStack = stack;
        gunStack.Shot += Shot;
        gunStack.ShotsGone += AutoReload;
        interval = gunStack.GunType.ReloadTime / gunStack.MaxShots;
        bar.UpdateFillAndFlash(gunStack.ShotsRemaining, gunStack.MaxShots);

        if (stack.ShotsRemaining == 0)
        {
            Reloading = true;
            AutoReloading = true;
        }
        else
            bar.SetDefaultColor();
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
        bar.SetDefaultColor();
        bar.UpdateFillAndFlash(gunStack.ShotsRemaining, gunStack.MaxShots);
    }

    void AutoReload()
    {
        StartReloading();
        AutoReloading = true;
        bar.SetColor(reloadingColor);
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
    float reloadTimer;
    void Update()
    {
        bar.transform.position = position.position;

        if (Reloading)
        {
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(reloadingSound.RandomSound());

            reloadTimer += Time.deltaTime;

            if (AutoReloading)
                bar.UpdateFill((reloadTimer + gunStack.ShotsRemaining + 1), gunStack.MaxShots);

            if (reloadTimer > interval)
            {
                gunStack.Reload();
                reloadTimer = 0;

                if (AutoReloading == false)
                    bar.UpdateFillAndFlash(gunStack.ShotsRemaining, gunStack.MaxShots);

                if (gunStack.FullyReloaded)
                {
                    if (AutoReloading)
                        bar.SetDefaultColor();

                    Reloading = false;
                    AutoReloading = false;
                    audioSource.Stop();
                    audioSource.PlayOneShot(finishedReloadingSound.RandomSound());
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
