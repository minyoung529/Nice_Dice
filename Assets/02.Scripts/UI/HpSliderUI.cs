using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSliderUI : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image fillImage;

    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, character.Hp / (float)character.MaxHp, 0.5f);
    }
}
