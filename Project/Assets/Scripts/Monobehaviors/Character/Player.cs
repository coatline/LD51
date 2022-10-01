using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ItemHolder itemHolder;

    void Awake()
    {
        itemHolder.ChangeItem(new GunStack(DataLibrary.I.Guns["Gun"], 1));
    }

    void Update()
    {
        
    }
}
