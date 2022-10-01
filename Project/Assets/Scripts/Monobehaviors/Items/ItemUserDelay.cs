using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemUser))]
public class ItemUserDelay : MonoBehaviour
{
    public bool CantUseItem { get; private set; }
    [SerializeField] ItemUser user;

    private void Start()
    {
        user.Used += Wait;
    }

    IEnumerator ItemUseTimer()
    {
        yield return new WaitForSeconds(useTime);
        CantUseItem = false;
    }

    float useTime;

    void Wait(float time)
    {
        useTime = time;
        CantUseItem = true;
        StartCoroutine(ItemUseTimer());
    }

    public void Respawn()
    {
        StopAllCoroutines();
        CantUseItem = false;
    }
}
