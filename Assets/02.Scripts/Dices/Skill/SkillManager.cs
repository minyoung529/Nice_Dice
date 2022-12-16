using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SkillManager : MonoBehaviour
{
    SkillUIController skillUIController;
    public static SkillBase CurrentSkill { get; set; }

    private void Awake()
    {
        EventManager<List<KeyValuePair<Dice, int>>>.StartListening(Define.ON_END_ROLL, Skill);
        EventManager.StartListening(Define.ON_RELOAD_GAME, OnReload);
    }

    private void Skill(List<KeyValuePair<Dice, int>> diceSides)
    {
        List<Dice> skillDices = new List<Dice>();

        foreach (var dice in diceSides)
        {
            if (dice.Key.DiceType == DiceType.Skill)
            {
                skillDices.Add(dice.Key);
            }
        }

        if (skillDices.Count == 0) return;

        foreach (Dice dice in skillDices)
        {
            SkillBase skill = Instantiate(dice.skill);
            CurrentSkill = skill;

            if (GameManager.Instance.PlayerTurn)
            {
                skill.SetCharacter(GameManager.Instance.Player);
            }
            else
            {
                skill.SetCharacter(GameManager.Instance.Enemy);
            }

            bool enemyHit = skill.EnemyHit;

            skillUIController.ShowSkill(dice, GameManager.Instance.PlayerTurn, enemyHit);
        }
    }

    private void OnReload()
    {
        skillUIController = FindObjectOfType<SkillUIController>();
    }

    private void OnDestroy()
    {
        EventManager<List<KeyValuePair<Dice, int>>>.StopListening(Define.ON_END_ROLL, Skill);
    }
}
