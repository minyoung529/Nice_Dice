using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField]
    private DiceData diceData = null;

    private List<Dice> selectedDice = new List<Dice>();
    private List<GameObject> diceObjects = new List<GameObject>();

    private const int MAX_PERCENTAGE = 100;
    private const int SKILL_PERCENTAGE = 20;
    private const int MULTIPLY_PERCENTAGE = 10;

    #region Property
    public IReadOnlyList<Dice> SelectedDice => selectedDice;
    public IReadOnlyList<GameObject> DiceObjects => diceObjects;
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
            selectedDice.Add(GameManager.Instance.Deck[n]);
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

        if (Random.Range(0, 100) > 30)
        {
            sideIdx = Random.Range(0, max);
        }

        return sideIdx;
    }

    private void DiceCreate()
    {
        diceObjects.Clear();

        Vector3 position = new Vector3(1.7f, -2.3f, 6.6f);

        for (int i = 0; i < 3; i++)
        {
            position.x += (i % 2 == 0 ? 1 : -1) * Random.Range(0f, 2.5f);
            position.z -= i != 2 ? i * 2 : i * 1.3f;

            GameObject dice = Instantiate(SelectedDice[i].DicePrefab, position, Quaternion.identity);
            diceObjects.Add(dice);
        }
    }

    public void DiceThrow()
    {
        DiceCreate();
        for (int i = 0; i < diceObjects.Count; i++)
        {
            DiceControl control = diceObjects[i].GetComponent<DiceControl>();
            control.DiceThrow();
        }
    }
}