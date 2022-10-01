using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    Item item;

    public void Setup(Item item)
    {
        this.item = item;
        sr.sprite = item.Sprite;
    }

    public Item Grab()
    {
        Destroy(gameObject);
        return item;
    }
}