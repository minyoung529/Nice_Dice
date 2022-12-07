using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class InventoryInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool IsInventoryInput { get; private set; }
    private Image image;
    private Color activeColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        activeColor = image.color;
        image.color = Color.clear;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsInventoryInput = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsInventoryInput = false;
    }

    public void Active()
    {
        gameObject.SetActive(true);
        image.DOKill();
        image.DOColor(activeColor, 1f);
    }

    public void Inactive()
    {
        image.DOKill();
        image.DOColor(Color.clear, 0.5f).OnComplete(()=> gameObject.SetActive(false));
    }
}