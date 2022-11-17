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
    private Vector3 vector = new Vector3(0, 1.5f, 1.5f);
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private bool isRotate = false;

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
            transform.Rotate(vector.x, vector.y, vector.z, Space.Self);
        }
    }

    public int DiceNumSelect(DiceShape shape, int grade)
    {
        int max = diceList[(int)shape].Length;
        return 0;
    }

    [ContextMenu("TestSideUp")]
    public void DiceSideUp(DiceShape shape = DiceShape.Cube, int sideIdx = 0)
    {
        isRotate = false;
        Vector3[] vectors = diceList[(int)shape];
        Vector3 upSide = vectors[sideIdx];
        transform.DORotate(upSide, speed * Time.deltaTime, RotateMode.Fast);
    }
}