using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGauge : MonoBehaviour
{
    private int Grade = 12; // �ܰ�, ���

    [SerializeField] private float speed = 3f;   // �ȵ� ���ǵ�

    [SerializeField] private GameObject graduation;   // ���� ������
    [SerializeField] private RectTransform fillImage;   // ä������ Image
    private float fillWidth;

    [SerializeField] private DiceManager diceManager;

    private bool isLeftMove;    // ���� �������� �����ִ��� �ƴ���

    private float curPosX = 0f;
    private float width = 0f;
    private float halfWidth = 0f;

    private bool isPlaying = false;
    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            if (value)
            {
                // �ʱ�ȭ�ϸ� ����
                ResetData();
            }
            else
            {
                int grade = CaculateGrade();
                for (int i = 0; i < 3; i++)
                {
                    Dice dice = diceManager.SelectedDice[i];
                    int side = diceManager.DiceSideSelect(dice.DiceShape, grade);
                    //dice.GetComponent<DiceControl>().DiceSideUp(dice.DiceShape, side);
                }
            }

            isPlaying = value;
        }
    }

    private float Rate => (curPosX + halfWidth) / width;

    private void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        halfWidth = width / 2;
        fillWidth = fillImage.rect.width;

        ResetData();

        // ���� ����
        for (int i = 1; i < Grade; i++)
        {
            GameObject newObj = Instantiate(graduation, transform);
            newObj.transform.position = GetPos(-halfWidth + (width / Grade) * i);
            newObj.SetActive(true);
        }
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

            Vector3 fillsize = fillImage.sizeDelta;
            fillsize.x = fillWidth * Rate;
            fillImage.sizeDelta = fillsize;
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
    private int CaculateGrade()
    {
        // 1~GRADE���� �ܰ谡 ���´�
        // 0���� �����ϹǷ� 1�� ������
        int grade = (int)(Rate / (1 / (float)Grade)) + 1;
        Debug.Log($"{grade}�ܰ�");
        return grade;
    }

    private void ResetData()
    {
        fillImage.sizeDelta *= Vector2.up;
        curPosX = -halfWidth;
    }
}
