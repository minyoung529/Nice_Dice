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

        bossText.text = $"처치한 보스: {bosses}";
        maxDamageText.text = $"최고 한방 대미지: {GameManager.maxDeal}";
    }
}
