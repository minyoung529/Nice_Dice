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

    public void UpdateUI(bool isHighScore, string boss, int maxDamage)
    {
        panel.gameObject.SetActive(true);

        int bossCnt = boss.Split(',').Length;

        highScore.SetActive(isHighScore);

        clearText.DOText($"{bossCnt} Monsters Clear!", 1f);
        bossText.text = $"óġ�� ����: {boss}";
        maxDamageText.text = $"�ְ� �ѹ� �����: {maxDamage}";
    }
}
