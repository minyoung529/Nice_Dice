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

    private List<Vector3[]> diceList = new List<Vector3[]>(); //DiceShape enum과 동일한 순서로 넣어줄것

    // 주사위들의 위로 가야하는 지점을 저장해두는 배열 모음 Region 
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
    /// 올릴 주사위 면을 고르는 함수 
    /// </summary>
    /// <param name="shape">주사위 모양</param>
    /// <param name="grade">랜덤 단계값</param>
    /// <param name="MAX_GRADE">최대 단계값</param>
    /// <returns></returns>
    public int DiceSideSelect(DiceShape shape, int grade, int MAX_GRADE = 12)
    {
        int max = diceList[(int)shape].Length;

        int section = MAX_GRADE / max;
        grade += section - 1;

        int val = grade / section;

        if (val == max) { val = 0; } // 0~11 이 아닌 1~12를 기준으로 계산하니까 끝값에 걸리면 인덱스에서 넘침, 고로 딱 그부분을 0으로 되돌리면 해결 WA!

        Debug.Assert(!(val >= max) || val > 0, $"val({val}) is out of index, grade({grade}), section({section})");

        if (Random.Range(0, 2) != 0) 
        {
            val = Random.Range(0, max);
        }

        return val;
    }

    /// <summary>
    /// 주사위 면을 올리는 함수
    /// </summary>
    /// <param name="shape">주사위 모양</param>
    /// <param name="sideIdx">올릴 주사위 면의 배열 인덱스</param>
    public void DiceSideUp(DiceShape shape = DiceShape.Cube, int sideIdx = 0)
    {
        isRotate = false;
        Vector3[] vectors = diceList[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, speed, RotateMode.Fast);
        Debug.Log($"{sideIdx} side is Up");
    }

    [ContextMenu("Test")]
    public void TestFunc() //테스트 용 함수 
    {
        int rand = Random.Range(0, 12); Debug.Log(rand);
        DiceSideUp(diceShape, DiceSideSelect(diceShape, rand));
    }
}