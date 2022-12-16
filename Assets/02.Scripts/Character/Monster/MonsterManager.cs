using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private List<Monster> monsters = null;
    [SerializeField]
    private HpSliderUI monsterHpUI = null;

    private Monster nowMonster = null;

    public Monster NowMonster
    {
        get
        {
            if (nowMonster == null)
            {
                nowMonster = FindObjectOfType<AIEnemyController>().monsterData;
            }
            return nowMonster;
        }
    }

    private void Awake()
    {
        EventManager.StartListening(Define.ON_NEXT_STAGE, NextStage);
        //EventManager.StartListening(Define.ON_END_GAME, ResetGame);
        EventManager.StartListening(Define.ON_RELOAD_GAME, ResetGame);
    }

    private void MonsterSetting(int idx)
    {
        GameObject gameObject = Instantiate(monsters[idx].MonsterPrefab);

        AIEnemyController monster = gameObject.GetComponent<AIEnemyController>();
        nowMonster = monster.monsterData;

        GameManager.Instance.Enemy = monster;
        monsterHpUI.Character = monster;

        monster.Enemy = GameManager.Instance.Player;
        monster.Hp = nowMonster.MAX_HP;

    }

    private void NextStage()
    {
        if (GameManager.Instance.stage <= monsters.Count)
        {
            MonsterSetting(GameManager.Instance.stage - 1);
        }
        else
        {
            MonsterSetting(Random.Range(0, monsters.Count));
        }
    }

    private void ResetGame()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].IsKnown = false;
        }

        monsterHpUI = GameObject.Find("MonsterGauge").GetComponent<HpSliderUI>();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_NEXT_STAGE, NextStage);
        //EventManager.StopListening(Define.ON_END_GAME, ResetGame);
        EventManager.StartListening(Define.ON_RELOAD_GAME, ResetGame);
    }
}
