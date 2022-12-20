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

    private void Awake()
    {
        EventManager.StartListening(Define.ON_RELOAD_GAME, Reload);
    }

    private void Start()
    {
        diceData.SettingData();
    }

    /// <summary>
    /// Dice를 덱에서 골라주는 함수. 스킬, 가중치 주사위 확률 적용 필요. 
    /// </summary>
    [ContextMenu("DiceSelect")]
    public void DiceSelect()
    {
        selectedDice.Clear();
        List<int> arr = new List<int>();
        bool isSkill = false;

        Debug.Log(GameManager.Instance.Data.CurrentUser.deck.Count);
        Debug.Log(GameManager.Instance.Data.CurrentUser.inventory.Count);

        for (int i = 0; i < Define.DICE_SELECT_COUNT; i++)
        {
            int n = Random.Range(0, GameManager.Instance.Deck.Count);

            while (arr.Contains(n) || (isSkill && GameManager.Instance.Deck[n].DiceType == DiceType.Skill))
            {
                n = Random.Range(0, GameManager.Instance.Deck.Count);
            }

            if (GameManager.Instance.Deck[n].DiceType == DiceType.Skill)
                isSkill = true;

            arr.Add(n);
            selectedDice.Add(GameManager.Instance.Deck[n]);
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

        if (Random.Range(0, 100) > 30)
        {
            sideIdx = Random.Range(0, max);
        }

        return sideIdx;
    }

    /// <summary>
    /// 선택된 Dice 오브젝트들을 생성하고, 생성된 오브젝트를 리스트에 넣어주어 관리할 수 있도록 만들어주는 함수
    /// </summary>
    public void DiceCreate(Vector3 rollPos, bool isLeft, IReadOnlyList<Dice> dices)
    {
        diceObjects.Clear();

        Vector3 position = rollPos; // 던지기 시작하는 포지션. 플레이어보다 왼쪽에 위치.

        for (int i = 0; i < 3; i++)
        {
            position.x += (i % 2 == 0 ? 1 : -1) * Random.Range(0f, 2.5f); // 홀 짝에 따라 움직일 방향 결정, 랜덤으로 x값(앞/뒤) 조정 
            float z = (i != 2) ? i * 2 : i * 1.3f; // i값에 따라 적당히 z(좌/우) 조정 

            if (!isLeft)
                z *= -1f;

            position.z -= z;

            GameObject dice = Instantiate(dices[i].DicePrefab, position, Quaternion.identity);
            diceObjects.Add(dice);
        }
    }

    public List<KeyValuePair<Dice, int>> RollRandomDice(int grade, Vector3 rollPos, bool isLeft, IReadOnlyList<Dice> dices = null)
    {
        if (dices == null)
            dices = SelectedDice;

        DiceCreate(rollPos, isLeft, dices);

        List<KeyValuePair<Dice, int>> selectedSides = new List<KeyValuePair<Dice, int>>();

        for (int i = 0; i < 3; ++i)
        {
            DiceShape shape = dices[i].DiceShape;
            int side = DiceSideSelect(shape, grade);

            DiceControl control = DiceObjects[i].GetComponent<DiceControl>();
            control.SetValue(shape, side);
            control.DiceThrow(isLeft);

            selectedSides.Add(new KeyValuePair<Dice, int>(dices[i], side));
        }

        return selectedSides;
    }

    private void Reload()
    {
        selectedDice.Clear();
        diceObjects.Clear();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_RELOAD_GAME, Reload);
    }
}