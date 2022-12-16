using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIController : MonoBehaviour
{
    [Header("SkillPanel")]
    [SerializeField] private Image skillPanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text infoText;
    [SerializeField] private Image skillIcon;

    [Header("Object")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform player;

    private void Start()
    {
        skillPanel.transform.localScale = Vector3.zero;

        //EventManager.StartListening(Define.ON_END_GAME, ResetValue);
        ResetValue();
    }

    public void ShowSkill(Dice dice, bool playerTurn, bool enemyHit)
    {
        nameText.text = dice.DiceName;
        infoText.text = dice.DiceDescription;
        skillIcon.sprite = dice.icon;

        Vector3 hitPos;


        if (playerTurn)
        {
            if (enemyHit)
                hitPos = enemy.position;
            else
                hitPos = player.position;

            skillPanel.transform.position = player.position + Vector3.up * 3f;
        }
        else
        {
            if (enemyHit)
                hitPos = player.position;
            else
                hitPos = enemy.position;

            skillPanel.transform.position = enemy.position + Vector3.up * 3f;
        }

        Sequence seq = DOTween.Sequence();

        seq.Append(skillPanel.transform.DOScale(Vector3.one, 0.3f));
        seq.AppendInterval(3f);

        seq.Append(skillPanel.transform.DOMove(hitPos, 0.4f).SetEase(Ease.InFlash));
        seq.Join(skillPanel.transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.OutFlash));

        seq.AppendCallback(() => EventManager.TriggerEvent(Define.ON_ACT_SKILL));
    }

    private void ResetValue()
    {
        enemy  = transform.Find("Enemy");
        player = transform.Find("Player");
    }
}
