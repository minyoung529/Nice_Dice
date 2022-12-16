using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyBox;
using DG.Tweening;

public class ShowImage : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Image image;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup ??= GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        for(int i = 0; i < sprites.Length;i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSeconds(3.5f);
        }


        canvasGroup ??= GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, 0.3f);

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }
}
