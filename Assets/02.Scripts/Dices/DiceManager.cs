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

    /// <summary>
    /// Dice�� ������ ����ִ� �Լ�. ��ų, ����ġ �ֻ��� Ȯ�� ���� �ʿ�. 
    /// </summary>
    [ContextMenu("DiceSelect")]
    public void DiceSelect()
    {
        selectedDice.Clear();
        List<int> arr = new List<int>();

        for (int i = 0; i < Define.DICE_SELECT_COUNT; i++)
        {
            int n = Random.Range(0, Define.MAX_DECK_COUNT);
            while (arr.Contains(n))
            {
                n = Random.Range(0, Define.MAX_DECK_COUNT);
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

    /// <summary>
    /// ���õ� Dice ������Ʈ���� �����ϰ�, ������ ������Ʈ�� ����Ʈ�� �־��־� ������ �� �ֵ��� ������ִ� �Լ�
    /// </summary>
    public void DiceCreate()
    {
        diceObjects.Clear();

        Vector3 position = new Vector3(1.7f, -2.3f, 6.6f); // ������ �����ϴ� ������. �÷��̾�� ���ʿ� ��ġ.

        for (int i = 0; i < 3; i++)
        {
            position.x += (i % 2 == 0 ? 1 : -1) * Random.Range(0f, 2.5f); // Ȧ ¦�� ���� ������ ���� ����, �������� x��(��/��) ���� 
            position.z -= i != 2 ? i * 2 : i * 1.3f; // i���� ���� ������ z(��/��) ���� 

            GameObject dice = Instantiate(SelectedDice[i].DicePrefab, position, Quaternion.identity);
            diceObjects.Add(dice);
        }
    }

    public List<KeyValuePair<Dice, int>> RollRandomDice(int grade)
    {
        DiceCreate();
        
        List<KeyValuePair<Dice, int>> selectedSides = new List<KeyValuePair<Dice, int>>();

        for (int i = 0; i < 3; ++i)
        {
            DiceShape shape = SelectedDice[i].DiceShape;
            int side = DiceSideSelect(shape, grade);

            DiceControl control = DiceObjects[i].GetComponent<DiceControl>();
            control.SetValue(shape, side);
            control.DiceThrow();

            selectedSides.Add(new KeyValuePair<Dice, int>(SelectedDice[i], side));
        }

        return selectedSides;
    }
}