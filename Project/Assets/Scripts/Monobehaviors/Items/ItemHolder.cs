using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public event System.Action<Item> SwitchedItem;
    public ItemStack ItemStack
    {
        get
        {
            if (itemDisplayer.ItemStack == null) return null;
            return itemDisplayer.ItemStack;
        }
    }
    public Item Item
    {
        get
        {
            if (itemDisplayer.ItemStack == null) return null;
            return itemDisplayer.ItemStack.Type;
        }
    }


    [SerializeField] SpriteRenderer muzzleFlash;
    [SerializeField] SpriteRenderer itemSprite;

    [SerializeField] Transform handTransform;
    [SerializeField] Transform handSprite;

    [SerializeField] RecoilAnimation recoil;
    [SerializeField] AimInputs aimInputs;
    [SerializeField] ItemUser itemUser;
    ItemDisplayer itemDisplayer;

    [SerializeField] float reach;

    public void ChangeItem(ItemStack stack)
    {
        if (itemDisplayer == null)
            itemDisplayer = new ItemDisplayer(null, true, null, itemSprite);

        itemDisplayer.ItemStack = stack;
        SwitchedItem?.Invoke(stack.Type);
        stack.Destroyed += StackDepleted;
    }

    void StackDepleted(ItemStack stack) => SwitchedItem?.Invoke(null);

    bool disabled;
    public void SetActive(bool active)
    {
        if (active && disabled == true)
        {
            disabled = false;
            itemUser.UseItemLock = false;
            handSprite.gameObject.SetActive(true);
        }
        else if (disabled == false)
        {
            disabled = true;
            itemUser.UseItemLock = true;
            handSprite.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Aims the hand and item towards designated position.
    /// </summary>
    /// <param name="toPosition">The position you want to aim at.</param>
    /// <param name="aimVariability">For variability in AI attacks.</param>
    public void Aim(Vector3 toPosition, Vector2 aimVariability)
    {
        float angle = Extensions.AngleFromPosition(transform.position, toPosition);

        angle += Random.Range((float)aimVariability.x, (float)aimVariability.y);

        float flip = 0;

        if (angle > 0 || angle < -180)
        {
            flip = 180;
        }

        toPosition.z = 0;
        Vector2 pos = (toPosition - transform.position).normalized * (Mathf.Clamp(Vector2.Distance(transform.position, toPosition), 0f, reach));

        handTransform.localPosition = pos;

        handTransform.transform.localRotation = Quaternion.Euler(0, 0, (angle + 90));
        handSprite.transform.localRotation = Quaternion.Euler(flip, 0, 0);
    }

    private void Update()
    {
        Aim(aimInputs.Position, Vector2.zero);
    }
}