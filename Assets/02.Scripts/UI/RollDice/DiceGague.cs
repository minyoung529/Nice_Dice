using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGague : MonoBehaviour
{
    private int Grade = 12; // �ܰ�, ���

    [SerializeField] private float speed = 3f;   // �ȵ� ���ǵ�
    [SerializeField] private Transform visual;   // �ȵ��Ÿ��� ������Ʈ

    [SerializeField] private GameObject graduation;   // ���� ������

    private bool isLeftMove;    // ���� �������� �����ִ��� �ƴ���

    private float curPosX = 0f;
    private float width = 0f;
    private float halfWidth = 0f;

    private bool isPlaying = true;
    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            if (value)
            {
                // �ʱ�ȭ�ϸ� ����
                curPosX = -halfWidth;
            }
            else
            {
                CaculateGrade();
            }

            isPlaying = value;
        }
    }

    private void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        halfWidth = width / 2;

        // ���� ����
        for (int i = 1; i < Grade; i++)
        {
            GameObject newObj = Instantiate(graduation, transform);
            newObj.transform.position = GetPos(-halfWidth + (width / Grade) * i);
            newObj.SetActive(true);
        }

        IsPlaying = true;
    }

    private void Update()
    {
        if (IsPlaying)
        {
            curPosX += Time.deltaTime * speed;

            // �� ���� �������� �� ������ �ٲ۴�
            if (curPosX > halfWidth || curPosX < -halfWidth)
            {
                ChangeDirection(!isLeftMove);
            }

            visual.position = GetPos(curPosX);
        }
    }

    // ���� ������ �ٲٴ� �Լ�
    private void ChangeDirection(bool isLeftMove)
    {
        this.isLeftMove = isLeftMove;

        if (isLeftMove)
            curPosX = halfWidth;
        else
            curPosX = -halfWidth;

        speed *= -1;
    }

    // ���� angle�� ��ġ
    private Vector3 GetPos(float curX)
    {
        return transform.position + Vector3.right * curX;
    }

    // ���(�ܰ�) �Ǵ� �Լ�
    private void CaculateGrade()
    {
        float rate = (curPosX + halfWidth) / width;
        // -angle���� angle���� curAngle�� ����

        // 1~GRADE���� �ܰ谡 ���´�
        // 0���� �����ϹǷ� 1�� ������
        int grade = (int)(rate / (1 / (float)Grade)) + 1;
        Debug.Log($"{grade}�ܰ�");
    }
}
