using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderUIController : MonoBehaviour
{
    public Text stageText = null;
    public Text enemyText = null;

    public void UpdateUI()
    {
        stageText.text = $"STAGE {GameManager.Instance.stage}";
        enemyText.text = GameManager.Instance.Enemy.GetComponent<AIEnemyController>().monsterData.MonsterName;
    }
}