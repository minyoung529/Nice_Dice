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
    private float throwPower = 3f;
    private Sequence throwSequence = null;

    private ParticleSystem particle = null;

    private DiceShape diceShape = DiceShape.Unknown;
    private int sideIdx = -1;

    public bool IsRotate
    {
        get { return isRotate; }
        set
        {
            if (!value)
            {
                // Rotate가 멈추면 Dice의 면을 올려줍니다.
                Vector3[] vectors = diceData.DiceShapeDict[(int)diceShape];
                Vector3 upSide = vectors[sideIdx];
                transform.DORotate(upSide, rotateSpeed, RotateMode.Fast);
                //Debug.Log($"{sideIdx} side is Up");
            }
            isRotate = value;
        }
    }
    public bool IsDrop => isDrop;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, DiceDestroy);
        EventManager.StartListening(Define.ON_START_PLAYER_TURN, DiceDestroy);
    }

    private void Update()
    {
        if (IsRotate)
        {
            transform.Rotate(rotateVector.x, rotateVector.y, rotateVector.z, Space.World);
        }
    }

    /// <summary>
    /// Dice를 던지는 함수. 플레이어를 기준으로 작성되었다. 
    /// 몬스터가 사용할 것이라면 endValue의 수정이 필요. 
    /// </summary>
    [ContextMenu("Throw")]
    public void DiceThrow(bool isLeft)
    {
        Vector3 endValue = Vector3.forward * 1.3f;

        if (!isLeft)
            endValue *= -1f;

        throwSequence = DOTween.Sequence()
       .Append(transform.DOJump(transform.position - endValue, throwPower, 1, 0.7f, false))
       .OnPlay(() =>
       {
           IsRotate = true;
           particle.Play();
       })
       .OnComplete(() =>
       {
           IsRotate = false;
           particle.Stop();
       });
    }

    /// <summary>
    /// Dice를 던지기 전, 값들을 세팅해준다. 
    /// </summary>
    /// <param name="diceShape">dice의 모양</param>
    /// <param name="side">올라갈 면의 idx</param>
    public void SetValue(DiceShape diceShape, int side)
    {
        this.diceShape = diceShape;
        sideIdx = side;
    }

    private void DiceDestroy()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, DiceDestroy);
        EventManager.StopListening(Define.ON_START_PLAYER_TURN, DiceDestroy);
    }
}
