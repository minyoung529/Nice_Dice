using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinUIController : MonoBehaviour
{
    public GameObject panel;

    public GameObject highScore;
    public Text clearText;
    public Text bossText;
    public Text maxDamageText;

    public Image winPanel;

    public void UpdateUI()
    {
        panel.gameObject.SetActive(true);
        string bosses = string.Join(", ", GameManager.Instance.ClearMonsters);
        int bossCnt = bosses.Split(',').Length;

        if (GameManager.Instance.Data.CurrentUser.SetHighScore(bossCnt))
        {
            highScore.SetActive(true);
        }

        clearText.text = "";
        clearText.DOText($"{GameManager.Instance.ClearMonsters.Count} Monsters Clear!", 1f);

        bossText.text = $"óġ�� ����: {bosses}";
        maxDamageText.text = $"�ְ� �ѹ� �����: {GameManager.maxDeal}";
    }
}
