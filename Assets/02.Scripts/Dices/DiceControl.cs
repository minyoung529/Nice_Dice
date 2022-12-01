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
    private Sequence throwSequence;

    private ParticleSystem particle = null;

    public bool IsRotate { get { return isRotate; } set { isRotate = value; } }
    public bool IsDrop => isDrop;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        throwSequence = DOTween.Sequence().Pause()
            .Append(transform.DOJump(transform.position - endValue, throwPower, 1, 0.7f, false))
            .OnPlay(() =>
            {
                IsRotate = true;
                particle.Play();
            })
            .OnComplete(() =>
            {
                particle.Stop();
            });
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
        isRotate = false;
        Vector3[] vectors = diceData.DiceShapeDict[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, rotateSpeed, RotateMode.Fast);
        Debug.Log($"{sideIdx} side is Up");
    }

    [ContextMenu("Throw")]
    public void DiceThrow()
    {
        throwSequence.Restart();
    }
}