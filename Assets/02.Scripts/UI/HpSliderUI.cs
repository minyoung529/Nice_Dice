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

    private readonly float TEXT_MOVE_DIST = 0.2f;

    private void Start()
    {
        prevHp = character.Hp;
    }

    public Character Character { set { character = value; } }

    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, character.Hp / (float)character.MaxHp, 0.5f);

        if (prevHp != character.Hp)
        {
            TextAnimation();
            damageText.text = (character.Hp - prevHp).ToString();
            prevHp = character.Hp;
        }
    }

    private void TextAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(damageText.transform.DOMoveY(damageText.transform.position.y - TEXT_MOVE_DIST, 0.5f));
        seq.AppendInterval(0.3f);
        seq.Append(damageText.DOColor(Color.clear, 0.3f));

        seq.AppendCallback(() => damageText.text = "");
        seq.Append(damageText.transform.DOMoveY(damageText.transform.position.y + TEXT_MOVE_DIST, 0f));
        seq.Append(damageText.DOColor(Color.white, 0f));
    }
}
