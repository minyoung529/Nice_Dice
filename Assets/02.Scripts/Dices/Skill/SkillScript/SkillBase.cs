using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillBase : MonoBehaviour
{
    [field:SerializeField]
    public bool EnemyHit { get; set; }
    protected Character character;
    protected Character enemy;

    public void SetCharacter(Character character)
    {
        this.character = character;
        enemy = character.Enemy;
    }

    private void Start()
    {
        EventManager.StartListening(Define.ON_START_PLAYER_TURN, OnNextTurn);
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, OnNextTurn);
        EventManager.StartListening(Define.ON_ACT_SKILL, OnActSkill);
        OnStart();
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnNextTurn()
    {

    }

    protected virtual void OnActSkill()
    {

    }

    protected virtual void ChildOnDestroy() { }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, OnNextTurn);
        EventManager.StopListening(Define.ON_START_PLAYER_TURN, OnNextTurn);
        EventManager.StopListening(Define.ON_ACT_SKILL, OnActSkill);
        SkillManager.CurrentSkill = null;
    }
}
