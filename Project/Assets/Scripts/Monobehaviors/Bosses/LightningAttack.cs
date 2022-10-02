using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] Sound sound;
    public void PlaySound() => source.PlayOneShot(sound.RandomSound());
    public void Destroy() => Destroy(gameObject);
}
