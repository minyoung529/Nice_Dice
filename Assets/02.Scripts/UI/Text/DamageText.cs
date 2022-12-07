using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TMP_Text damageText;

    private void Start()
    {
        damageText = GetComponent<TMP_Text>();
    }

    public void Text(string text)
    {
        StartCoroutine(TextCoroutine(text));
    }

    private IEnumerator TextCoroutine(string text)
    {
        damageText.text = text;
        yield return new WaitForSeconds(2.7f);
        damageText.text = "";
    }
}
