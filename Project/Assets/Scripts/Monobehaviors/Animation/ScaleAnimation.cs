using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : AnimationBase
{
    [SerializeField] Transform targetTransform;
    [SerializeField] Vector2 stretchScale;
    [SerializeField] Vector2 defaultScale;
    [SerializeField] float speed;

    protected override void Animate()
    {
        targetTransform.localScale = Vector3.Lerp(defaultScale, stretchScale, currentTime);
    }
}
