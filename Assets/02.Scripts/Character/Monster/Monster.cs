using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Odd,
    Even,
    Range,
    Unknown
}

[CreateAssetMenu(fileName = "New Monster", menuName = "SO/Monster/MonsterInstance")]
public class Monster : ScriptableObject
{
    [SerializeField] private string monsterName = "";
    [TextArea]
    [SerializeField] private List<string> descriptionList = new List<string>();

    [SerializeField] private MonsterType monsterType = MonsterType.Unknown;
    [SerializeField] private GameObject monsterPrefab = null;
    [SerializeField] private int Max_Hp = 100;
    [SerializeField] private int hp = 100;
    [SerializeField] private List<Dice> monsterDices = new List<Dice>();

    [ConditionalField(nameof(monsterType), false, MonsterType.Range)]
    [SerializeField] private int minDamage = 1;
    [ConditionalField(nameof(monsterType), false, MonsterType.Range)]
    [SerializeField] private int maxDamage = 100;

    #region Property
    public string MonsterName => monsterName;
    public IReadOnlyList<string> DescriptionList => descriptionList;
    public MonsterType MonsterType => monsterType;
    public GameObject MonsterPrefab => monsterPrefab;
    public int MAX_HP => Max_Hp;
    public int Hp => hp;
    public int MinDamage => minDamage;
    public int MaxDamage => maxDamage;
    public IReadOnlyList<Dice> MonsterDices => monsterDices;
    #endregion

    public Monster(string name, string[] description, int maxHp, MonsterType monsterType)
    {
        monsterName = name;
        for (int i = 0; i < description.Length; i++)
        {
            descriptionList.Add(description[i]);
        }
        Max_Hp = maxHp;
        hp = Max_Hp;
        this.monsterType = monsterType;
    }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(monsterName))
        {
            monsterName = name;
        }
    }
}