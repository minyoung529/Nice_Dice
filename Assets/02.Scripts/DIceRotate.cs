using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceRotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 vector = new Vector3(0, 1, 1);
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private bool IsRotate = false;

    void Update()
    {
        if (IsRotate)
        {
            transform.Rotate(vector.x, vector.y, vector.z, Space.Self);
        }
    }
}