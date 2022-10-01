using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ItemHolder itemHolder;
    Pickup currentPickup;

    void Awake()
    {
        ChangeItem(DataLibrary.I.Guns["Gun"]);
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

    void ChangeItem(Item i) => itemHolder.ChangeItem(new GunStack(i as Gun, 1));
}
