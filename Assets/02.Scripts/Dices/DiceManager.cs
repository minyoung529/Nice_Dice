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
    /// �ø� �ֻ��� ���� ���� �Լ� 
    /// </summary>
    /// <param name="shape">�ֻ��� ���</param>
    /// <param name="grade">���� �ܰ谪</param>
    /// <param name="MAX_GRADE">�ִ� �ܰ谪</param>
    /// <returns></returns>
    public int DiceSideSelect(DiceShape shape, int grade, int MAX_GRADE = 12)
    {
        int max = diceData.DiceShapeDict[(int)shape].Length; // �ִ�� ���� �� �ε��� 

        int section = MAX_GRADE / max; // ���� 
        grade += section - 1; // �������� ���� �ø� ���� ���� ����. ����� ���� ������

        int sideIdx = grade / section;

        if (sideIdx >= max) { sideIdx = 0; } // CaculateGrade���� 1�� ���� �Ѱ��ֱ� ������ ��� �Ŀ� ��ġ�� �κ��� ����. �� �κ��� �� ������ �ٲپ��ش�. 

        Debug.Assert(!(sideIdx >= max) || sideIdx > 0, $"val({sideIdx}) is out of index. grade({grade}), section({section})"); // �ε������� ����� ����� ����ó��

        if (Random.Range(0, 2) != 0)
        {
            sideIdx = Random.Range(0, max);
        }

        return sideIdx;
    }
}