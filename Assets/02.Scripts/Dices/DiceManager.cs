using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField]
    private DiceData diceData = null;

    [SerializeField] private List<Dice> diceDeck = new List<Dice>();
    [SerializeField] private List<Dice> selectedDice = new List<Dice>();

    private const int MAX_PERCENTAGE = 100;
    private const int SKILL_PERCENTAGE = 20;
    private const int MULTIPLY_PERCENTAGE = 10;

    #region Property
    public IReadOnlyList<Dice> DiceDeck => diceDeck;
    public IReadOnlyList<Dice> SelectedDice => selectedDice;
    #endregion

    private void Start()
    {
        diceData.SettingData();
    }

    [ContextMenu("DiceSelect")]
    public void DiceSelect()
    {
        selectedDice.Clear();
        List<int> arr = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int n = Random.Range(0, 6);
            while (arr.Contains(n))
            {
                n = Random.Range(0, 6);
            }
            arr.Add(n);
            selectedDice.Add(diceDeck[n]);
        }
    }

    /// <summary>
    /// 올릴 주사위 면을 고르는 함수 
    /// </summary>
    /// <param name="shape">주사위 모양</param>
    /// <param name="grade">랜덤 단계값</param>
    /// <param name="MAX_GRADE">최대 단계값</param>
    /// <returns></returns>
    public int DiceSideSelect(DiceShape shape, int grade, int MAX_GRADE = 12)
    {
        int max = diceData.DiceShapeDict[(int)shape].Length; // 최대로 나올 면 인덱스 

        int section = MAX_GRADE / max; // 구간 
        grade += section - 1; // 나눗셈을 통해 올릴 면을 구할 예정. 계산을 위해 더해줌

        int sideIdx = grade / section;

        if (sideIdx >= max) { sideIdx = 0; } // CaculateGrade에서 1을 더해 넘겨주기 때문에 계산 후에 넘치는 부분이 생김. 그 부분을 맨 앞으로 바꾸어준다. 

        Debug.Assert(!(sideIdx >= max) || sideIdx > 0, $"val({sideIdx}) is out of index. grade({grade}), section({section})"); // 인덱스에서 벗어나는 경우의 예외처리

        if (Random.Range(0, 2) != 0)
        {
            sideIdx = Random.Range(0, max);
        }

        return sideIdx;
    }
}