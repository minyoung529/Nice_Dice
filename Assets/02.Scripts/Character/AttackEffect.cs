using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    LowAttack,
    HighAttack,
    FinalAttack,
    Shield,
    Length
}

public class AttackEffect : MonoBehaviour
{
    public GameObject[] effects = new GameObject[(int)EffectType.Length];
}