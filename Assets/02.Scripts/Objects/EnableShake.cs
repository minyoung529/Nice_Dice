using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class EnableShake : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    public void Text(string str)
    {
        text.text = str;
    }

    public void Active()
    {
        gameObject.SetActive(true);
        transform.DOShakePosition(1.5f, 15).OnComplete(() => { text.text = ""; gameObject.SetActive(false); });
    }
}