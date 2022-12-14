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
                // Rotate�� ���߸� Dice�� ���� �÷��ݴϴ�.
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
    /// Dice�� ������ �Լ�. �÷��̾ �������� �ۼ��Ǿ���. 
    /// ���Ͱ� ����� ���̶�� endValue�� ������ �ʿ�. 
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
    /// Dice�� ������ ��, ������ �������ش�. 
    /// </summary>
    /// <param name="diceShape">dice�� ���</param>
    /// <param name="side">�ö� ���� idx</param>
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
