using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDiceUI : MonoBehaviour
{
    private GameObject diceUI = null;
    private GameObject[] dicePanel = new GameObject[3];
    private List<GameObject> diceObjects = new List<GameObject>();

    private void Awake()
    {
        diceUI = transform.GetChild(0).gameObject;
        for (int i = 0; i < diceUI.transform.childCount; i++)
        {
            dicePanel[i] = diceUI.transform.GetChild(i).gameObject;
        }

        EventManager.StartListening(Define.ON_END_DRAW, ActiveUI);
        EventManager.StartListening(Define.ON_END_DRAW, CreateDice);
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, InactiveUI);
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, DestroyDice);
    }

    private void InactiveUI()
    {
        diceUI.SetActive(false);
    }

    private void ActiveUI()
    {
        diceUI.SetActive(true);
    }

    private void CreateDice()
    {
        for (int i = 0; i < dicePanel.Length; i++)
        {
            Vector3 pos = dicePanel[i].transform.position;
            pos.z -= 1f;
            GameObject newObject = GameManager.Instance.Pool.Pop(GameManager.Instance.Dice.SelectedDice[i].DicePrefab);
            newObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
            newObject.transform.localScale = Vector3.one * 0.5f;

            Roll roll = newObject.GetComponent<Roll>();

            if (!roll)
            {
                newObject.AddComponent<Roll>();
            }
            newObject.GetComponent<DiceControl>().enabled = false;
            diceObjects.Add(newObject);
        }
    }

    private void DestroyDice()
    {
        for (int i = 0; i < /*dicePanel.Length*/diceObjects.Count; i++)
        {
            diceObjects[i].transform.localScale = Vector3.one;
            GameManager.Instance.Pool.Push(diceObjects[i]);
        }
        diceObjects.Clear();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_DRAW, ActiveUI);
        EventManager.StopListening(Define.ON_END_DRAW, CreateDice);
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, InactiveUI);
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, DestroyDice);
    }
}