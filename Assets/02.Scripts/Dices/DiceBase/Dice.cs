using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyBox;

public enum DiceType
{
    Number,
    Multiply,
    Skill,
    Unknown
}
public enum DiceShape
{
    Cube = 6,
    /// <summary>
    /// 정사면체
    /// </summary>
    Tetrahedron = 4,
    /// <summary>
    /// 정팔면체
    /// </summary>
    Octahedron = 8,
    Unknown
}

[System.Serializable]
public class VisibleList<T>
{
    [SerializeField]
    private List<T> list;
    public List<T> List => list;

    public VisibleList(int size)
    {
        list = new List<T>();

        for (int i = 0; i < size; i++)
            list.Add(default(T));
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                Debug.LogError("Out Of Range!");
                return default(T);
            }

            return list[index];
        }
        set
        {
            if (index < 0 || index >= Count)
            {
                Debug.LogError("Out Of Range!");
            }
            else
                list[index] = value;
        }
    }

    public int Count => list.Count;

    void Push(T val) => list.Add(val);
    void Remove(T item) => list.Remove(item);
    void RemoveAt(int idx) => list.RemoveAt(idx);
}

[CreateAssetMenu(fileName = "New Dice", menuName = "SO/Dice/DiceInstance")]
public class Dice : ScriptableObject
{
    [Separator("Dice Variable")]

    [SerializeField] private string diceName = "";
    [SerializeField] private string diceDescription = "";
    [SerializeField] private DiceShape diceShape = DiceShape.Unknown;
    [SerializeField] private DiceType diceType = DiceType.Unknown;

    [Separator("Dice Sides")]

    [ConditionalField(nameof(diceType), false, DiceType.Number, DiceType.Multiply)]
    public VisibleList<int> numbers;
    [ConditionalField(nameof(diceType), false, DiceType.Skill)]
    public VisibleList<int> skills;

    #region Property
    public string DiceName { get => diceName; set => diceName = value; }
    public string DiceDescription { get => diceDescription; set => diceDescription = value; }
    public DiceType DiceType { get => diceType; set => diceType = value; }
    public DiceShape DiceShape { get => diceShape; set => diceShape = value; }
    #endregion

    public Dice(string diceName, string diceDescription, DiceType diceType, DiceShape diceShape)
    {
        this.diceName = diceName;
        this.diceDescription = diceDescription;
        this.diceType = diceType;
        this.diceShape = diceShape;
    }

    public object GetSide(int index)
    {
        if (numbers.Count >= index || skills.Count >= index) return null;

        if (diceType == DiceType.Skill)
        {
            return skills[index];
        }

        return numbers[index];
    }

    public void SetNumberSide(int index, object newSide)
    {
        if (numbers.Count >= index || skills.Count >= index) return;

        if (diceType == DiceType.Skill)
        {
            skills[index] = (int)newSide;
        }

        numbers[index] = (int)newSide;
    }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(diceName))
        {
            diceName = name;
        }

        if(diceType == DiceType.Skill && (skills==null || skills.Count != (int)diceShape))
        {
            skills = new VisibleList<int>((int)diceShape);
        }
        else if((diceType == DiceType.Number || diceType == DiceType.Multiply) &&
            (numbers == null || numbers.Count != (int)diceShape))
        {
            numbers = new VisibleList<int>((int)diceShape);
        }
    }
}