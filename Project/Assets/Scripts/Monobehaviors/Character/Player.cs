using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ItemHolder itemHolder;
    Pickup currentPickup;

    void Start()
    {
        itemHolder.ChangeItem(new GunStack(DataLibrary.I.Guns["Starter Pistol"], 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
            currentPickup = collision.gameObject.GetComponent<Pickup>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            Pickup p = collision.gameObject.GetComponent<Pickup>();
            if (p == currentPickup)
                currentPickup = null;
        }
    }

    public void TryPickup()
    {
        if (currentPickup)
            ChangeItem(currentPickup.Grab());
    }

    void ChangeItem(ItemStack i)
    {
        if (itemHolder.Item != null)
            PickupSpawner.I.SpawnItem(itemHolder.ItemStack, transform.position, true);
        itemHolder.ChangeItem(i);
    }
}
