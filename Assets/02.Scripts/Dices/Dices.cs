using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dice List", menuName = "SO/Dice/Dices")]
public class Dices : ScriptableObject
{
    public List<Dice> dices;
}