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
        if (GameManager.Instance.Enemy != null)
        {
            enemyText.text = GameManager.Instance.Enemy.GetComponent<AIEnemyController>().monsterData.MonsterName;
            return;
        }
     
        enemyText.text = "¾â¶ó²á ¹®¾î";
    }
}