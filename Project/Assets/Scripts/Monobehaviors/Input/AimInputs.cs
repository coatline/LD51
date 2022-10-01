using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimInputs : MonoBehaviour
{
    const float AIM_DEADZONE = .05f;

    [SerializeField] Transform characterTransform;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Player player;
    Camera cam;

    bool keyboard;

    private void Awake()
    {
        cam = Camera.main;
        keyboard = playerInput.currentControlScheme == "Keyboard&Mouse";
    }

    Vector2 position;

    public void OnAim(InputAction.CallbackContext value)
    {
        Vector2 val = value.ReadValue<Vector2>();

        if (keyboard)
            position = val;
        else if (val.magnitude > AIM_DEADZONE)
            position = new Vector3(val.x, val.y);
    }

    public void CheckIsKeyboard()
    {
        keyboard = playerInput.currentControlScheme == "Keyboard&Mouse";
    }

    public Vector2 Position
    {
        get
        {
            Vector2 pos = position;

            if (keyboard)
                pos = cam.ScreenToWorldPoint(position);
            else
                pos += new Vector2(characterTransform.position.x, characterTransform.position.y);

            return pos;
        }
    }
}
