using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class DeckUIController : MonoBehaviour
{
    public static EquipPanel CurrentEquipPanel { get; set; }

    private List<InventoryPanel> panels = new List<InventoryPanel>();
    private List<EquipPanel> equipPanels = new List<EquipPanel>();
    public FollowTarget[] diceObjects = new FollowTarget[100];

    [field: SerializeField]
    public InfoPanel InfoPanel { get; set; }

    public InventoryInput InventoryInput { get; set; }

    [SerializeField] private InventoryPanel samplePanel;

    private void Awake()
    {
        InventoryInput = FindObjectOfType<InventoryInput>();
        equipPanels = new List<EquipPanel>(FindObjectsOfType<EquipPanel>());

        //SetDeck(GameManager.Instance.Deck);

        // 패널 생성
        for (int i = 0; i < GameManager.Instance.Inventory.Count; i++)
        {
            InventoryPanel panel = Instantiate(samplePanel, samplePanel.transform.parent);
            panels.Add(panel);

            panel.Init(i, GameManager.Instance.Inventory[i]);
        }

        for (int i = 0; i < GameManager.Instance.Deck.Count; i++)
        {
            InventoryPanel panel = Instantiate(samplePanel, samplePanel.transform.parent);
            panels.Add(panel);

            panel.Init(i + GameManager.Instance.Inventory.Count, GameManager.Instance.Deck[i]);
        }

        samplePanel.gameObject.SetActive(false);
    }

    private IEnumerator Coroutine()
    {
        yield return null;

        int invCnt = GameManager.Instance.Inventory.Count;

        for (int i = 0; i < invCnt; i++)
        {
            CreateDiceObject(i, GameManager.Instance.Inventory[i].DicePrefab);
        }

        for (int i = invCnt; i < invCnt + GameManager.Instance.Deck.Count; i++)
        {
            CreateDiceObject(i, GameManager.Instance.Deck[i - invCnt].DicePrefab);
        }

        yield return null;

        SetDeck(GameManager.Instance.Deck);
    }

    private void Start()
    {
        StartCoroutine(Coroutine());
    }

    /// <summary>
    /// 주사위 오브젝트 만들기
    /// </summary>
    private void CreateDiceObject(int index, GameObject prefab)
    {
        if (diceObjects[index] != null)
        {
            Destroy(diceObjects[index].gameObject);
        }

        Vector3 pos = panels[index].transform.position;
        pos.z = 0f;

        diceObjects[index] = Instantiate(prefab, pos, Quaternion.Euler(-22.5f, 45f, -22.5f), null).AddComponent<FollowTarget>();
        diceObjects[index].transform.localScale *= 12f;
        diceObjects[index].gameObject.AddComponent<Roll>();
        diceObjects[index].ChangeTarget(panels[index].transform);
    }

    private void SetDeck(List<Dice> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int idx = i + GameManager.Instance.Inventory.Count;

            EquipPanel panel = GetEmptyPanel(deck[i].DiceType);

            if (panel)
            {
                if (panel.EquipDice(deck[i], idx, false))
                {
                    panels[idx].Equip(panel);
                }
            }
        }
    }

    private EquipPanel GetEmptyPanel(DiceType type)
    {
        return equipPanels.Find(x => x.IsEmpty && x.equipDiceType == type);
    }
}
