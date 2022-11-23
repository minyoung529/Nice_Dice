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
    /// 정사면체
    /// </summary>
    Tetrahedron,
    /// <summary>
    /// 정팔면체
    /// </summary>
    Octahedron,
    Unknown,
}

public class Dice : MonoBehaviour
{
    private string diceName = "";
    private string diceDescription = "";
    private DiceType diceType = DiceType.Unknown;
    private DiceShape diceShape = DiceShape.Unknown;

    public string DiceName => diceName;
    public string DiceDescription => diceDescription;
    public DiceType DiceType => diceType;
    public DiceShape DiceShape => diceShape;
}