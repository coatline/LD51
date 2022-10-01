using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationBase : MonoBehaviour
{
    protected float PercentComplete => currentTime / duration;

    [SerializeField] protected float duration;
    [SerializeField] bool playOnAwake;
    [SerializeField] bool loop;
    protected float currentTime;
    bool running;

    private void Awake()
    {
        if (playOnAwake)
            Play();
    }

    void Update()
    {
        if (running)
        {
            currentTime += Time.deltaTime;
            Animate();

            if (currentTime > duration)
            {
                if (loop)
                    Play();
                else
                    Stop();
            }
        }
    }

    protected abstract void Animate();

    public virtual void Play()
    {
        running = true;
        currentTime = 0;
    }

    public virtual void PlayAlready()
    {
        running = true;
    }

    public virtual void Stop()
    {
        running = false;
        currentTime = 0;
    }
}
