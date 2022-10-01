using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    const float DOWN_PLATFORM_DEADZONE = .5f;

    [SerializeField] UnityEvent DecendPlatform;

    public float XMovement { get; private set; }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 val = value.ReadValue<Vector2>();

        XMovement = Mathf.MoveTowards(0, 1, Mathf.Abs(val.x) / .5f);

        if (val.x < 0)
            XMovement *= -1;

        if (val.y < -DOWN_PLATFORM_DEADZONE)
            DecendPlatform?.Invoke();
    }
}
