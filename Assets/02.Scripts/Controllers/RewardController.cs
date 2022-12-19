using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RewardController : MonoBehaviour
{
    [SerializeField]
    private string dicesPath = "AllDices";
    [SerializeField]
    private string inventoryPath = "Inventory";

    [SerializeField]
    private GameObject panel = null;
    [SerializeField]
    private List<Button> buttons = new List<Button>();
    [SerializeField]
    private List<TMP_Text> nameTexts = new List<TMP_Text>();

    private Dices allDices = null;
    private Dices inventory = null;
    private const int REWARD_AMOUNT = 3;

    private List<Dice> rewards = new List<Dice>();
    public List<Dice> Rewards => rewards;

    private void Awake()
    {
        allDices = Resources.Load<Dices>(dicesPath);
        inventory = Resources.Load<Dices>(inventoryPath);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].onClick.AddListener(() => RewardGive(i));
        }

        EventManager.StartListening(Define.ON_END_GAME, PrepareReward);
    }


    private void RandomReward()
    {
        for (int i = 0; i < allDices.dices.Count; i++)
        {
            rewards.Add(allDices.dices[Random.Range(0, allDices.dices.Count)]);
        }
    }

    private void RewardGive(int selectedIdx)
    {
        inventory.dices.Add(allDices.dices[selectedIdx]);
        panel.SetActive(false);
    }

    private void ShowDice()
    {
        panel.SetActive(true);
        Debug.Log("Call");
        for (int i = 0; i < REWARD_AMOUNT; i++)
        {
            GameObject gameObject = Instantiate(rewards[i].DicePrefab, buttons[i].transform);
            gameObject.transform.Translate(0f, 0f, -10f);
            gameObject.transform.localScale = Vector3.one * 175f;
            gameObject.GetComponent<DiceControl>().enabled = false;
            gameObject.AddComponent<Roll>();
            nameTexts[i].text = rewards[i].DiceName;
        }
    }

    private void PrepareReward()
    {
        rewards.Clear();
        RandomReward();
        Invoke("ShowDice", 2.5f);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_GAME, PrepareReward);
    }
}