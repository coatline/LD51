using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithBarriers : MonoBehaviour
{
    [SerializeField] Vector2 bottomLeftBarrierCoords;
    [SerializeField] Vector2 topRightBarrierCoords;

    [SerializeField] bool offsetBarrierPositionsToFitCameraSize;

    [SerializeField] Transform followObject;
    Vector2 cameraSizeInUnits;

    [SerializeField] bool doBarriers = true;

    [Range(.01f, 1f)]
    [SerializeField] float speed = 1f;

    private void Awake()
    {
        var cam = GetComponent<Camera>();

        cameraSizeInUnits.x = cam.orthographicSize * cam.aspect;
        cameraSizeInUnits.y = cam.orthographicSize;
    }

    void FixedUpdate()
    {
        if (!followObject) { return; }

        Vector3 movement = new Vector3(followObject.position.x - transform.position.x, followObject.position.y - transform.position.y);

        Vector3 blbc = bottomLeftBarrierCoords;
        Vector3 trbc = topRightBarrierCoords;

        if (doBarriers)
        {
            if (offsetBarrierPositionsToFitCameraSize)
            {
                blbc += new Vector3((cameraSizeInUnits.x), cameraSizeInUnits.y, 0);
                trbc -= new Vector3((cameraSizeInUnits.x), cameraSizeInUnits.y, 0);
            }
        }

        Vector3 newPos = (transform.position + (movement * speed));

        if (doBarriers) { newPos = new Vector3(Mathf.Clamp(newPos.x, blbc.x, trbc.x), Mathf.Clamp(newPos.y, blbc.y, trbc.y), -10); }

        transform.position = newPos;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void SetFollowObject(Transform follow) => followObject = follow;
}
