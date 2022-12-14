using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상수 매니저
/// </summary>
public class Define
{
    #region LAYER_MASK

    #endregion

    #region EVENT
    public const int ON_START_PLAYER_TURN = 100;
    public const int ON_START_MONSTER_TURN = 101;

    public const int ON_START_DRAW = 1002;
    public const int ON_END_DRAW = 1003;

    public const int ON_END_ROLL = 2000;

    public const int ON_END_GAME = 10000;
    public const int ON_ACT_SKILL = 125;

    public const int ON_NEXT_STAGE = 3000;
    public const int ON_RELOAD_GAME = 3100;
    public const int ON_HIT = 111;
    #endregion

    public const int MAX_NUMBER_DICE = 3;
    public const int MAX_SKILL_DICE = 2;
    public const int MAX_MULTIPLY_DICE = 1;
    public const int DICE_SELECT_COUNT = 3;

    public const int MAX_DECK_COUNT = 4;
}