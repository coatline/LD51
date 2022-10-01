using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public static class Extensions
{
    public static float AngleFromPosition(Vector3 pivotPosition, Vector3 pos)
    {
        float angleRad = Mathf.Atan2(pos.y - pivotPosition.y, pos.x - pivotPosition.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 90;
        return angleDeg;
    }

    static string GetName()
    {
        int num = Random.Range(0,7);

        switch (num)
        {
            case 0: return "Bryler";
            case 1: return "Ender";
            case 3: return "David";
            case 4: return "Fred";
            case 5: return "Jaquan";
            case 6: return "Mum";
            default: return "porky";
        }

    }
}

