using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform target;
    public Transform Target { get => target; set { target = value; SetFirstPosition(target?.position); } }
    private Vector3 prevPosition;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Vector3 followPos;

        if (target)
        {
            followPos = target.position;
        }
        else
        {
            transform.position = GetMousePosition();
            return;
        }

        Vector3 curDiff = followPos - prevPosition;
        curDiff.z = 0f;
        transform.position += curDiff;

        prevPosition = followPos;
    }

    public void ChangeTarget(Transform followTransform)
    {
        if (followTransform)
        {
            Target = followTransform;
        }
        else
        {
            Target = null;
        }
    }

    private void SetFirstPosition(Vector3? pos)
    {
        if (pos == null)
        {
            pos = GetMousePosition();
        }

        Vector3 myPos = pos.Value;
        myPos.z = transform.position.z;

        prevPosition = pos.Value;
        transform.position = myPos;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = camera.farClipPlane;

        mousePos = camera.ScreenToWorldPoint(mousePos);
        mousePos.z = transform.position.z;

        return mousePos;
    }
}
