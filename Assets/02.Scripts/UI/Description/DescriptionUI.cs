using DG.Tweening;
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

    private bool isTexting = false;

    private void Start()
    {
        monsterManager = FindObjectOfType<MonsterManager>();
        uiPanel.GetComponent<Button>().onClick.AddListener(() => Description());
        EventManager.StartListening(Define.ON_NEXT_STAGE, DelayActivePanel);
    }

    public void ActivePanel(float delay = 0f)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            Debug.Log("acti");
            if (monsterManager.NowMonster.IsKnown) { return; }
            //Time.timeScale = 0f;

            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            Description();
            monsterManager.NowMonster.IsKnown = true;
        });
    }

    private void DelayActivePanel()
    {
        ActivePanel(2f);
    }

    private void UpdateText()
    {
        if (descriptionIdx < monsterManager.NowMonster.DescriptionList.Count)
        {
            descriptionText.text = "";
            isTexting = true;
            descriptionText.DOText(monsterManager.NowMonster.DescriptionList[descriptionIdx], 3f).OnComplete(() => isTexting = false);
            //descriptionText.text = monsterManager.NowMonster.DescriptionList[descriptionIdx];
            return;
        }
    }

    private void Description()
    {
        if(isTexting)
        {
            descriptionText.DOKill();
            descriptionText.text = monsterManager.NowMonster.DescriptionList[descriptionIdx-1];
            isTexting = false;
            return;
        }
        else if (descriptionIdx < monsterManager.NowMonster.DescriptionList.Count)
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
        //Time.timeScale = 1f;
        GameStart.StartGame();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_NEXT_STAGE, DelayActivePanel);
    }
}