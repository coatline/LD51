using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss", fileName = "New Booss")]

public class Boss : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject prefab;
    [SerializeField] int expOrbs;

    public GameObject Prefab => prefab;
    public Sprite Sprite => sprite;
    public int ExpOrbs => expOrbs;
}
