using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    const float JUMP_DEADZONE = .175f;
    const float FIRE_DEADZONE = .175f;
    const float DOWN_PLATFORM_DEADZONE = .5f;

    [SerializeField] ReloadBehavior reloadBehavior;
    [SerializeField] UnityEvent DecendPlatform;
    [SerializeField] GameObject settings;
    [SerializeField] ItemUser itemUser;
    [SerializeField] Jumper jumper;
    [SerializeField] Player player;

    public float XMovement { get; private set; }

    public void OnPause(InputAction.CallbackContext value)
    {
        float val = value.ReadValue<float>();

        if (value.performed == false) return;
        if (val >= 0)
            settings.SetActive(!settings.activeSelf);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 val = value.ReadValue<Vector2>();

        XMovement = Mathf.MoveTowards(0, 1, Mathf.Abs(val.x) / .5f);

        if (val.x < 0)
            XMovement *= -1;

        if (val.y < -DOWN_PLATFORM_DEADZONE)
            DecendPlatform?.Invoke();
    }

    bool jumpButtonDown;

    public void OnJump(InputAction.CallbackContext value)
    {
        float val = value.ReadValue<float>();

        if (val >= JUMP_DEADZONE)
        {
            if (jumpButtonDown && val < .9f)
            {
                jumpButtonDown = false;
                jumper.ReleaseJumpButton();
            }
            else
                jumpButtonDown = true;
        }
        else if (jumpButtonDown)
        {
            jumpButtonDown = false;
            jumper.ReleaseJumpButton();
        }
    }

    bool fireButtonDown;

    public void OnFire(InputAction.CallbackContext value)
    {
        float val = value.ReadValue<float>();

        if (val >= FIRE_DEADZONE && value.performed)
        {
            fireButtonDown = true;
        }
        else if (value.canceled)
        {
            fireButtonDown = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        float val = value.ReadValue<float>();

        if (value.performed == false) return;
        if (val >= 0)
            player.TryPickup();
    }

    public void OnReload(InputAction.CallbackContext value)
    {
        float val = value.ReadValue<float>();

        if (value.performed == false) return;
        if (val >= 0)
            reloadBehavior.StartReloading();
    }

    private void Update()
    {
        if (jumpButtonDown)
            jumper.PressJump();

        if (fireButtonDown)
            itemUser.TryUseItem();
    }
}
