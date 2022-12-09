using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    new Camera camera;
    private Vector3 originalPos;
    private Vector3 originalRot;

    private readonly Vector3 DRAW_POS = new Vector3(-47f, -1f, 0f);
    private readonly Vector3 DRAW_ROT = new Vector3(0f, 90f, 0f);

    private void Awake()
    {
        EventManager.StartListening(Define.ON_START_DRAW, MoveDrawPos);
        EventManager.StartListening(Define.ON_END_DRAW, MoveOriginalPos);
    }

    void Start()
    {
        camera = GetComponent<Camera>();
        originalPos = transform.position;
        originalRot = transform.eulerAngles;
    }

    private void MoveOriginalPos()
    {
        Move(originalPos, 1f);
        Rotate(originalRot, 1f);
        Zoom(4f, 1f);
    }

    private void MoveDrawPos()
    {
        Move(DRAW_POS, 1f);
        Rotate(DRAW_ROT, 1f);
        Zoom(1f, 1f);
    }

    public void Move(Vector3 pos, float duration)
    {
        camera.transform.DOMove(pos, duration);
    }

    public void Rotate(Vector3 angles, float duration)
    {
        camera.transform.DORotate(angles, duration);
    }

    public void Zoom(float size, float duration)
    {
        camera.DOOrthoSize(size, duration);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_DRAW, MoveDrawPos);
        EventManager.StopListening(Define.ON_END_DRAW, MoveOriginalPos);
    }
}