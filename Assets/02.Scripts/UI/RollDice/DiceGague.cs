using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGague : MonoBehaviour
{
    private readonly int GRADE = 12;
    [SerializeField] private Transform center;   // 원의 중심

    [SerializeField] private float speed = 3f;   // 똑딱 스피드
    [SerializeField] private Transform visual;   // 똑딱거리는 오브젝트

    [SerializeField] private GameObject graduation;   // 눈금 프리팹

    private bool isLeftMove;    // 현재 왼쪽으로 가고있는지 아닌지
    private float curAngle;     // 현재 각도
    public float radius;        // 원의 반지름
    private float angle;        // 똑딱거릴 최대 각도

    private bool isPlaying = true;
    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            if (value)
            {
                // 초기화하며 시작
                curAngle = -angle;
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
        radius = Vector2.Distance(transform.position, center.position);

        Vector3 direction = transform.position - center.position;
        angle = 90f - Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 눈금 생성
        for (int i = 1; i < GRADE; i++)
        {
            GameObject newObj = Instantiate(graduation, transform);
            newObj.transform.position = GetPos(-angle + (angle * 2 / GRADE) * i);
            newObj.transform.up = GetDirection(-angle + (angle * 2 / GRADE) * i);
            newObj.SetActive(true);
        }

        IsPlaying = true;
    }

    private void Update()
    {
        if (IsPlaying)
        {
            curAngle += Time.deltaTime * speed;

            // 각 끝에 도달했을 때 방향을 바꾼다
            if (curAngle > angle || curAngle < -angle)
            {
                ChangeDirection(!isLeftMove);
            }

            visual.position = GetPos(curAngle);
            visual.up = GetDirection(curAngle);
        }
    }

    // 현재 방향을 바꾸는 함수
    private void ChangeDirection(bool isLeftMove)
    {
        this.isLeftMove = isLeftMove;

        if (isLeftMove)
            curAngle = angle;
        else
            curAngle = -angle;

        speed *= -1;
    }

    // 현재 angle의 방향
    private Vector3 GetDirection(float angle)
    {
        Vector3 direction = Vector3.zero;
        direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
        direction.y = Mathf.Cos(angle * Mathf.Deg2Rad);

        return direction;
    }

    // 현재 angle의 위치
    private Vector3 GetPos(float angle)
    {
        return center.position + GetDirection(angle) * radius;
    }

    // 등급(단계) 판단 함수
    private void CaculateGrade()
    {
        float rate =  (curAngle + angle) / (angle * 2f);
        // -angle부터 angle까지 curAngle의 비율

        // 1~GRADE까지 단계가 나온다
        // 0부터 시작하므로 1을 더해줌
        int grade = (int)(rate / (1 / (float)GRADE)) + 1;
        Debug.Log($"{grade}단계");
    }

    private void OnDrawGizmosSelected()
    {
        radius = Vector2.Distance(transform.position, center.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(center.transform.position, GetPos(angle));
    }
}
