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
    private DiceShape diceshape = DiceShape.Unknown; // MEMO : 나중에 Dice 완성되면 삭제. 그쪽을 통해 접근하도록 하기 
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private bool isRotate = false; // 회전 중인지 아닌지 
    [SerializeField]
    private bool isDrop = false; // 낙인지 아닌지 
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
    /// 올릴 주사위 면을 고르는 함수 
    /// </summary>
    /// <param name="shape">주사위 모양</param>
    /// <param name="grade">랜덤 단계값</param>
    /// <param name="MAX_GRADE">최대 단계값</param>
    /// <returns></returns>
    public int DiceSideSelect(DiceShape shape, int grade, int MAX_GRADE = 12)
    {
        int max = diceData.DiceShapeList[(int)shape].Length; // 최대로 나올 면 인덱스 

        int section = MAX_GRADE / max; // 구간 
        grade += section - 1; // 나눗셈을 통해 올릴 면을 구할 예정. 계산을 위해 더해줌

        int sideIdx = grade / section;

        if (sideIdx >= max) { sideIdx = 0; } // CaculateGrade에서 1을 더해 넘겨주기 때문에 계산 후에 넘치는 부분이 생김. 그 부분을 맨 앞으로 바꾸어준다. 

        Debug.Assert(!(sideIdx >= max) || sideIdx > 0, $"val({sideIdx}) is out of index. grade({grade}), section({section})"); // 인덱스에서 벗어나는 경우의 예외처리

        if (Random.Range(0, 2) != 0)
        {
            sideIdx = Random.Range(0, max);
        }

        return sideIdx;
    }

    /// <summary>
    /// 주사위 면을 올리는 함수
    /// </summary>
    /// <param name="shape">주사위 모양</param>
    /// <param name="sideIdx">올릴 주사위 면의 배열 인덱스</param>
    public void DiceSideUp(DiceShape shape = DiceShape.Cube, int sideIdx = 0)
    {
        isRotate = false;
        Vector3[] vectors = diceData.DiceShapeList[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, speed, RotateMode.Fast);
        Debug.Log($"{sideIdx} side is Up");
    }
}