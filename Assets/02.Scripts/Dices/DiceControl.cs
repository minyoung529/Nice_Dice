using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class DiceControl : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotateVector = new Vector3(0, 2f, 2f);
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private bool isRotate = false; // ȸ�� ������ �ƴ��� 
    [SerializeField]
    private bool isDrop = false; // ������ �ƴ��� 
    [SerializeField]
    private DiceData diceData = null; // SO

    public bool IsRotate { get { return isRotate; } set { isRotate = value; } }
    public bool IsDrop => isDrop;

    private void Update()
    {
        if (IsRotate)
        {
            transform.Rotate(rotateVector.x, rotateVector.y, rotateVector.z, Space.World);
        }
    }

    /// <summary>
    /// �ֻ��� ���� �ø��� �Լ�
    /// </summary>
    /// <param name="shape">�ֻ��� ���</param>
    /// <param name="sideIdx">�ø� �ֻ��� ���� �迭 �ε���</param>
    public void DiceSideUp(DiceShape shape = DiceShape.Cube, int sideIdx = 0)
    {
        isRotate = false;
        Vector3[] vectors = diceData.DiceShapeList[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, speed, RotateMode.Fast);
        Debug.Log($"{sideIdx} side is Up");
    }
}