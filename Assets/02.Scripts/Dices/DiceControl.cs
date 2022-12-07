using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class DiceControl : MonoBehaviour
{
    [SerializeField]
    private DiceData diceData = null; // SO
    [SerializeField]
    private bool isRotate = false; // ȸ�� ������ �ƴ��� 
    [SerializeField]
    private bool isDrop = false; // ������ �ƴ��� 

    [Header("Rotate")]
    [SerializeField]
    private Vector3 rotateVector = new Vector3(0, 2.5f, 2.5f);
    [SerializeField]
    private float rotateSpeed = 1f;

    [Header("Throw")]
    [SerializeField]
    private Vector3 endValue = new Vector3(0f, 0f, 4.5f);
    [SerializeField]
    private float throwPower = 3f;
    private Sequence throwSequence = null;

    private ParticleSystem particle = null;

    public bool IsRotate { get { return isRotate; } set { isRotate = value; } }
    public bool IsDrop => isDrop;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

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
        Vector3[] vectors = diceData.DiceShapeDict[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, rotateSpeed, RotateMode.Fast);
        Debug.Log($"{sideIdx} side is Up");
    }

    /// <summary>
    /// Dice�� ������ �Լ�. �÷��̾ �������� �ۼ��Ǿ���. 
    /// ���Ͱ� ����� ���̶�� endValue�� ������ �ʿ�. 
    /// </summary>
    [ContextMenu("Throw")]
    public void DiceThrow(DiceShape shape = DiceShape.Unknown, int side = -1)
    {
        throwSequence = DOTween.Sequence()
           .Append(transform.DOJump(transform.position - endValue, throwPower, 1, 0.7f, false))
           .OnPlay(() =>
           {
               IsRotate = true;
               particle.Play();
           })
           .OnComplete(() =>
           {
               isRotate = false;
               particle.Stop();
               DiceSideUp(shape, side);
           });
    }
}