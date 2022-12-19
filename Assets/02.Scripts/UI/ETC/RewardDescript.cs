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


    private void Awake()
    {
        rewardController = FindObjectOfType<RewardController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Dice dice = rewardController.Rewards[buttonIdx];

        diceName.text = dice.DiceName;
        diceDescription.text = dice.DiceDescription;

        Vector3 vector = transform.position;
        vector.x += vector.x <= 0 ? 465f : -465f;
        vector.y = -200f;
        vector.z -= 5f;
        descriptionPanel.transform.position = vector;

        descriptionPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
}