using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class RewardDescript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RewardController rewardController = null;

    [SerializeField]
    private int buttonIdx = -1;
    [SerializeField]
    private GameObject descriptionPanel = null;
    [SerializeField]
    private TMP_Text diceName = null;
    [SerializeField]
    private TMP_Text diceDescription = null;

    private RectTransform panelRectTransform = null; //descriptionPanel

    private void Awake()
    {
        rewardController = FindObjectOfType<RewardController>();
        panelRectTransform = descriptionPanel.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Dice dice = rewardController.Rewards[buttonIdx];

        diceName.text = dice.DiceName;
        diceDescription.text = dice.DiceDescription;

        descriptionPanel.transform.position = transform.position;
        float x = 0;
        x = panelRectTransform.anchoredPosition.x <= 0f ? -425f : 425f;
        panelRectTransform.anchoredPosition -= new Vector2(x, 280f);

        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
}