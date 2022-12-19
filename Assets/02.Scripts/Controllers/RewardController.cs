using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private Vector3 dicePosition = new Vector3(6f, 0f, -1350f);


    private List<GameObject> diceObject = new List<GameObject>();
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
        GameManager.Instance.Data.CurrentUser.inventory.Add(rewards[selectedIdx]);

        for (int i = 0; i < REWARD_AMOUNT; i++)
        {
            Destroy(diceObject[i]);
        }
        panel.SetActive(false);
    }

    private void ShowDice()
    {
        panel.SetActive(true);
        for (int i = 0; i < REWARD_AMOUNT; i++)
        {
            dicePosition = buttons[i].transform.position;
            dicePosition.z -= 10f;
            GameObject gameObject = Instantiate(rewards[i].DicePrefab, dicePosition, Quaternion.identity);

            gameObject.transform.localScale = Vector3.one * 2.5f;
            gameObject.GetComponent<DiceControl>().enabled = false;
            gameObject.AddComponent<Roll>();
            nameTexts[i].text = rewards[i].DiceName;
            diceObject.Add(gameObject);
        }
    }

    private void PrepareReward()
    {
        rewards.Clear();
        diceObject.Clear();
        RandomReward();
        Invoke("ShowDice", 2.5f);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_GAME, PrepareReward);
    }

}