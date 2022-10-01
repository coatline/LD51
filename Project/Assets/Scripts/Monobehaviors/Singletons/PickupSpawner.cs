using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : Singleton<PickupSpawner>
{
    [SerializeField] Pickup pickupPrefab;
    [SerializeField] GameObject experiecePrefab;

    public void SpawnExp(Vector2 pos)
    {
        Instantiate(experiecePrefab, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    public void SpawnItem(Item i, int count, Vector3 pos, bool doForce = true)
    {
        Pickup pickup = Instantiate(pickupPrefab, pos, Quaternion.identity, transform);
        pickup.Setup(i);
    }

    public void SpawnItem(ItemPackage ip, Vector3 pos, bool doForce = true)
    {
        Pickup pickup = Instantiate(pickupPrefab, pos, Quaternion.identity, transform);
        pickup.Setup(ip.Item);
    }

    public void SpawnItems(ItemPool itemPool, Vector3 pos, bool doForce = true)
    {
        ItemPackage[] items = itemPool.GetItems();

        for (int k = 0; k < items.Length; k++)
        {
            Pickup pickup = Instantiate(pickupPrefab, pos, Quaternion.identity, transform);
            pickup.Setup(items[k].Item);
        }
    }

    private void Start()
    {
        SpawnItem(DataLibrary.I.Guns["Gun"], 1, Vector3.zero);
        SpawnExp(new Vector2(-4, 0));
    }
}
