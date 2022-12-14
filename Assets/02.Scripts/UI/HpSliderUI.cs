using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpSliderUI : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private TMP_Text damageText;

    int prevHp;

    private readonly float TEXT_MOVE_DIST = 0.05f;

    private void Start()
    {
        prevHp = character.Hp;
    }

    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, character.Hp / (float)character.MaxHp, 0.5f);

        if (prevHp != character.Hp)
        {
            TextAnimation();
        }
    }

    private void TextAnimation()
    {
        Sequence seq = DOTween.Sequence();
        damageText.text = (character.Hp - prevHp).ToString();
        seq.Append(damageText.transform.DOMoveY(damageText.transform.position.y - TEXT_MOVE_DIST, 0.3f));
        seq.Append(damageText.DOColor(Color.clear, 0.3f));

        seq.AppendCallback(() => damageText.text = "");
        seq.Append(damageText.transform.DOMoveY(damageText.transform.position.y + TEXT_MOVE_DIST, 0f));
        seq.Append(damageText.DOColor(Color.clear, 0f));
    }
}
