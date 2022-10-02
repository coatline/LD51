using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    ItemStack item;

    public void Setup(ItemStack item)
    {
        this.item = item;
        sr.sprite = item.Type.Sprite;
    }

    public ItemStack Grab()
    {
        Destroy(gameObject);
        return item;
    }
}