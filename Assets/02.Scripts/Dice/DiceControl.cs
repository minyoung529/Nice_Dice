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
    private DiceShape diceshape = DiceShape.Unknown; // MEMO : ���߿� Dice �ϼ��Ǹ� ����. ������ ���� �����ϵ��� �ϱ� 
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private bool isRotate = false; // ȸ�� ������ �ƴ��� 
    [SerializeField]
    private bool isDrop = false; // ������ �ƴ��� 
    [SerializeField]
    private DiceData diceData = null; // SO

    public bool IsRotate => isRotate;
    public bool IsDrop => isDrop;

    private void Update()
    {
        if (IsRotate)
        {
            transform.Rotate(rotateVector.x, rotateVector.y, rotateVector.z, Space.World);
        }
    }

    /// <summary>
    /// �ø� �ֻ��� ���� ���� �Լ� 
    /// </summary>
    /// <param name="shape">�ֻ��� ���</param>
    /// <param name="grade">���� �ܰ谪</param>
    /// <param name="MAX_GRADE">�ִ� �ܰ谪</param>
    /// <returns></returns>
    public int DiceSideSelect(DiceShape shape, int grade, int MAX_GRADE = 12)
    {
        int max = diceData.DiceShapeList[(int)shape].Length; // �ִ�� ���� �� �ε��� 

        int section = MAX_GRADE / max; // ���� 
        grade += section - 1; // �������� ���� �ø� ���� ���� ����. ����� ���� ������

        int sideIdx = grade / section;

        if (sideIdx >= max) { sideIdx = 0; } // CaculateGrade���� 1�� ���� �Ѱ��ֱ� ������ ��� �Ŀ� ��ġ�� �κ��� ����. �� �κ��� �� ������ �ٲپ��ش�. 

        Debug.Assert(!(sideIdx >= max) || sideIdx > 0, $"val({sideIdx}) is out of index. grade({grade}), section({section})"); // �ε������� ����� ����� ����ó��

        if (Random.Range(0, 2) != 0)
        {
            sideIdx = Random.Range(0, max);
        }

        return sideIdx;
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