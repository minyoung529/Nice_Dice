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

    public void ActivePanel()
    {
        if (monsterManager.NowMonster.IsKnown) { return; }
        Time.timeScale = 0f;

        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        
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
        
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        descriptionIdx = 0;
        Time.timeScale = 1f;
    }

}