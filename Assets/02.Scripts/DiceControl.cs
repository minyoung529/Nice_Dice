using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceControl : MonoBehaviour
{
    public bool IsRotate = false;
    [SerializeField]
    private Vector3 vector = new Vector3(0, 1, 1);
    [SerializeField]
    private float speed = 100f;

    void Update()
    {
        if (IsRotate)
        {
            transform.Rotate(vector.x, vector.y, vector.z, Space.Self);
        }
    }
}