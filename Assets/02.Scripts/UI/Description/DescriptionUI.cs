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

    [SerializeField]
    private AudioClip openSound;

    [SerializeField]
    private AudioClip typingSound;

    [SerializeField]
    private AudioClip bgmClip;

    private GameStart gameStart;

    private void Start()
    {
        monsterManager = FindObjectOfType<MonsterManager>();
        gameStart = FindObjectOfType<GameStart>();
        uiPanel.GetComponent<Button>().onClick.AddListener(() => Description());
        EventManager.StartListening(Define.ON_NEXT_STAGE, DelayActivePanel);
    }

    public void ActivePanel(float delay = 2f)
    {
        SoundManager.Instance.Play(AudioType.BGM, bgmClip);

        Sequence seq = DOTween.Sequence();

        SoundManager.Instance.PlayOneshot(openSound);
        uiPanel.transform.DOScaleY(0f, 0f);
        uiPanel.transform.DOScaleY(1f, 0.7f);

        seq.AppendInterval(delay);
        seq.AppendCallback(() =>
        {
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

            SoundManager.Instance.Play(AudioType.Effect, typingSound);

            string info = monsterManager.NowMonster.DescriptionList[descriptionIdx];
            descriptionText.DOText(info, info.Length * 0.08f).OnComplete(() =>
            {
                isTexting = false;
                SoundManager.Instance.Stop(AudioType.Effect);
            });
            //descriptionText.text = monsterManager.NowMonster.DescriptionList[descriptionIdx];
            return;
        }
    }

    private void Description()
    {
        if (isTexting)
        {
            descriptionText.DOKill();
            SoundManager.Instance.Stop(AudioType.Effect);
            descriptionText.text = monsterManager.NowMonster.DescriptionList[descriptionIdx - 1];
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
        gameStart.StartGame();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_NEXT_STAGE, DelayActivePanel);
    }
}