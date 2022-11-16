using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceControl : MonoBehaviour
{
    public enum DiceShape
    {
        Cube,
    }

    public bool IsRotate
    {
        get { return isRotate; }
        set { isRotate = value; }
    }

    [SerializeField]
    private Vector3 vector = new Vector3(0, 1, 1);
    [SerializeField]
    private float speed = 100f;

    private bool isRotate = false;
    private List<Vector3[]> diceList = new List<Vector3[]>(); //DiceShape enum�� ������ ������ �־��ٰ�

    // �ֻ������� ���� �����ϴ� ������ �����صδ� �迭 ���� Region 
    #region DiceArray 
    private Vector3[] cubeDice = new Vector3[] { new Vector3(0, 0, 0), new Vector3(90, 0, 0), new Vector3(0, 0, -90), new Vector3(0, 0, 90), new Vector3(-90, 0, 0), new Vector3(180, 0, 0) };
    #endregion

    private void Awake()
    {
        diceList.Add(cubeDice);
    }

    void Update()
    {
        if (IsRotate)
        {
            transform.Rotate(vector.x, vector.y, vector.z, Space.Self);
        }
    }

}