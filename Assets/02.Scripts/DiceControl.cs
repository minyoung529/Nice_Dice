using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public enum DiceShape
{
    Cube,
    Unknown,
}

public class DiceControl : MonoBehaviour
{

    public bool IsRotate
    {
        get { return isRotate; }
        set { isRotate = value; }
    }

    [SerializeField]
    private Vector3 rotateVector = new Vector3(0, 1.5f, 1.5f);
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private bool isRotate = false;
    [SerializeField]
    private bool isDrop = false;

    [SerializeField]
    private DiceShape diceShape = DiceShape.Unknown;

    private List<Vector3[]> diceList = new List<Vector3[]>(); //DiceShape enum�� ������ ������ �־��ٰ�

    // �ֻ������� ���� �����ϴ� ������ �����صδ� �迭 ���� Region 
    #region DiceArray 
    private Vector3[] cubeDice = new Vector3[] { new Vector3(0, 0, 0), new Vector3(90, 0, 0), new Vector3(0, 0, -90), new Vector3(0, 0, 90), new Vector3(-90, 0, 0), new Vector3(180, 0, 0) };
    #endregion

    private void Awake()
    {
        diceList.Add(cubeDice);
    }

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
        int max = diceList[(int)shape].Length;

        int section = MAX_GRADE / max;
        grade += section - 1;

        int val = grade / section;

        if (val == max) { val = 0; } // 0~11 �� �ƴ� 1~12�� �������� ����ϴϱ� ������ �ɸ��� �ε������� ��ħ, ��� �� �׺κ��� 0���� �ǵ����� �ذ� WA!

        Debug.Assert(!(val >= max) || val > 0, $"val({val}) is out of index, grade({grade}), section({section})");

        if (Random.Range(0, 2) != 0) 
        {
            val = Random.Range(0, max);
        }

        return val;
    }

    /// <summary>
    /// �ֻ��� ���� �ø��� �Լ�
    /// </summary>
    /// <param name="shape">�ֻ��� ���</param>
    /// <param name="sideIdx">�ø� �ֻ��� ���� �迭 �ε���</param>
    public void DiceSideUp(DiceShape shape = DiceShape.Cube, int sideIdx = 0)
    {
        isRotate = false;
        Vector3[] vectors = diceList[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, speed, RotateMode.Fast);
        Debug.Log($"{sideIdx} side is Up");
    }

    [ContextMenu("Test")]
    public void TestFunc() //�׽�Ʈ �� �Լ� 
    {
        int rand = Random.Range(0, 12); Debug.Log(rand);
        DiceSideUp(diceShape, DiceSideSelect(diceShape, rand));
    }
}