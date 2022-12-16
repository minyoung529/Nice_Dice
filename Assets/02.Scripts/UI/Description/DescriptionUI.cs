using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescriptionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject uiPanel = null;
    [SerializeField]
    private TMP_Text descriptionText = null;

    private MonsterManager monsterManager = null;
    private int descriptionIdx = 0;


    private void Awake()
    {
        monsterManager = FindObjectOfType<MonsterManager>();
        uiPanel.GetComponent<Button>().onClick.AddListener(() => Description());
        EventManager.StartListening(Define.ON_NEXT_STAGE, ActivePanel);
    }

    private void ActivePanel()
    {
        if (monsterManager.NowMonster.IsKnown) { return; }
        uiPanel.SetActive(true);
        Description();
        monsterManager.NowMonster.IsKnown = true;
    }

    private void UpdateText()
    {
        if (descriptionIdx < monsterManager.NowMonster.DescriptionList.Count)
        {
            descriptionText.text = monsterManager.NowMonster.DescriptionList[descriptionIdx];
            return;
        }
    }

    private void Description()
    {
        if (descriptionIdx < monsterManager.NowMonster.DescriptionList.Count)
        {
            UpdateText();
            descriptionIdx++;
            return;
        }

        descriptionIdx = 0;
        uiPanel.SetActive(false);
    }

}