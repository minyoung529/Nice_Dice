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

    Vector3 originalPos;

    private readonly float TEXT_MOVE_DIST = 0.2f;

    private void Start()
    {
        prevHp = character.Hp;
        originalPos = damageText.transform.position;
    }

    public Character Character { set { character = value; } }

    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, character.Hp / (float)character.MaxHp, 0.5f);

        if (prevHp != character.Hp)
        {
            damageText.text = (character.Hp - prevHp).ToString();
            TextAnimation();
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
        seq.AppendCallback(() => damageText.transform.position = originalPos);
        seq.AppendCallback(() => damageText.color = Color.white);
    }
}
