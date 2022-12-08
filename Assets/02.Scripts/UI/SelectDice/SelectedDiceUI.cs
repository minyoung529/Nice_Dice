using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDiceUI : MonoBehaviour
{
    private GameObject diceUI = null;
    private GameObject[] dicePanel = new GameObject[3];
    private List<GameObject> diceObjects = new List<GameObject>();
    private GameManager gameManager = null;

    private void Awake()
    {
        diceUI = transform.GetChild(0).gameObject;
        for (int i = 0; i < diceUI.transform.childCount; i++)
        {
            dicePanel[i] = diceUI.transform.GetChild(i).gameObject;
        }

        gameManager = FindObjectOfType<GameManager>();
        EventManager.StartListening(Define.ON_END_DRAW, ActiveUI);
        EventManager.StartListening(Define.ON_END_DRAW, CreateDice);
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, ActiveUI);
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, DestroyDice);
    }

    private void ActiveUI()
    {
        diceUI.SetActive(!diceUI.activeSelf);
    }

    private void CreateDice()
    {
        for (int i = 0; i < dicePanel.Length; i++)
        {
            Vector3 pos = dicePanel[i].transform.position;
            pos.z -= 1f;
            GameObject newObject = Instantiate(gameManager.Dice.SelectedDice[i].DicePrefab, pos, Quaternion.identity, null);
            newObject.transform.localScale = Vector3.one * 0.5f;
            newObject.AddComponent<Roll>();
            newObject.GetComponent<DiceControl>().enabled = false;
            diceObjects.Add(newObject);
        }
    }

    private void DestroyDice()
    {
        for (int i = 0; i < dicePanel.Length; i++)
        {
            Destroy(diceObjects[i]);
        }
        diceObjects.Clear();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_DRAW, CreateDice);
        EventManager.StopListening(Define.ON_END_DRAW, ActiveUI);
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, DestroyDice);
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, ActiveUI);
    }
}