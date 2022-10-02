using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : Singleton<PickupSpawner>
{
    [SerializeField] Pickup pickupPrefab;
    [SerializeField] Rigidbody2D experiecePrefab;

    public void SpawnExp(Vector2 pos)
    {
        Rigidbody2D rb = Instantiate(experiecePrefab, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        rb.angularVelocity = Random.Range(0, 361f);
        rb.AddForce(new Vector2(Random.Range(-250f, 250f), Random.Range(-150f, 350f)));
    }

    public void SpawnItem(ItemStack i, Vector3 pos, bool doForce = true)
    {
        Pickup pickup = Instantiate(pickupPrefab, pos, Quaternion.identity, transform);
        pickup.Setup(i);
    }

    public void SpawnItems(ItemPool itemPool, Vector3 pos, bool doForce = true)
    {
        ItemPackage[] items = itemPool.GetItems();

        for (int k = 0; k < items.Length; k++)
        {
            Pickup pickup = Instantiate(pickupPrefab, pos, Quaternion.identity, transform);
            pickup.Setup(new ItemStack(items[k].Item, 1));
        }
    }

    private void Start()
    {
        SpawnItem(new GunStack(DataLibrary.I.Guns["Gun"], 1), Vector3.zero);
    }
}
