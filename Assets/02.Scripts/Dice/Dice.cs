using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceType
{
    Number,
    Multiply,
    Skill,
    Unknown
}
public enum DiceShape
{
    Cube,
    /// <summary>
    /// �����ü
    /// </summary>
    Tetrahedron,
    /// <summary>
    /// ���ȸ�ü
    /// </summary>
    Octahedron,
    Unknown,
}

public class Dice : MonoBehaviour
{
    [SerializeField] private string diceName = "";
    [SerializeField] private string diceDescription = "";
    [SerializeField] private DiceType diceType = DiceType.Unknown;
    [SerializeField] private DiceShape diceShape = DiceShape.Unknown;

    #region Property
    public string DiceName => diceName;
    public string DiceDescription => diceDescription;
    public DiceType DiceType => diceType;
    public DiceShape DiceShape => diceShape;
    #endregion
}