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
    private bool isRotate = false; // 회전 중인지 아닌지 
    [SerializeField]
    private bool isDrop = false; // 낙인지 아닌지 

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
    /// 주사위 면을 올리는 함수
    /// </summary>
    /// <param name="shape">주사위 모양</param>
    /// <param name="sideIdx">올릴 주사위 면의 배열 인덱스</param>
    public void DiceSideUp(DiceShape shape = DiceShape.Cube, int sideIdx = 0)
    {
        Vector3[] vectors = diceData.DiceShapeDict[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, rotateSpeed, RotateMode.Fast);
        Debug.Log($"{sideIdx} side is Up");
    }

    /// <summary>
    /// Dice를 던지는 함수. 플레이어를 기준으로 작성되었다. 
    /// 몬스터가 사용할 것이라면 endValue의 수정이 필요. 
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