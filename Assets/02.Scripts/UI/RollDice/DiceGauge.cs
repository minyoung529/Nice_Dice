using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGauge : MonoBehaviour
{
    private int Grade = 12; // 단계, 등급

    [SerializeField] private float speed = 3f;   // 똑딱 스피드

    [SerializeField] private GameObject graduation;   // 눈금 프리팹
    [SerializeField] private RectTransform fillImage;   // 채워지는 Image
    private float fillWidth;

    [SerializeField] private DiceManager diceManager;

    private bool isLeftMove;    // 현재 왼쪽으로 가고있는지 아닌지

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
                // 초기화하며 시작
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

        // 눈금 생성
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

            // 각 끝에 도달했을 때 방향을 바꾼다
            if (curPosX > halfWidth || curPosX < -halfWidth)
            {
                ChangeDirection(!isLeftMove);
            }

            Vector3 fillsize = fillImage.sizeDelta;
            fillsize.x = fillWidth * Rate;
            fillImage.sizeDelta = fillsize;
        }
    }

    // 현재 방향을 바꾸는 함수
    private void ChangeDirection(bool isLeftMove)
    {
        this.isLeftMove = isLeftMove;

        if (isLeftMove)
            curPosX = halfWidth;
        else
            curPosX = -halfWidth;

        speed *= -1;
    }

    // 현재 angle의 위치
    private Vector3 GetPos(float curX)
    {
        return transform.position + Vector3.right * curX;
    }

    // 등급(단계) 판단 함수
    private int CaculateGrade()
    {
        // 1~GRADE까지 단계가 나온다
        // 0부터 시작하므로 1을 더해줌
        int grade = (int)(Rate / (1 / (float)Grade)) + 1;
        Debug.Log($"{grade}단계");
        return grade;
    }

    private void ResetData()
    {
        fillImage.sizeDelta *= Vector2.up;
        curPosX = -halfWidth;
    }
}
