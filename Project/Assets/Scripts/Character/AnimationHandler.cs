using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    int moveAnimBool;

    void Awake()
    {
        moveAnimBool = Animator.StringToHash("Moving");
    }

    public void StartedMoving() => animator.SetBool(moveAnimBool, true);
    public void StoppedMoving() => animator.SetBool(moveAnimBool, false);
}
