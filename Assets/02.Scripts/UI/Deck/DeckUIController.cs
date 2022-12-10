using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class DeckUIController : MonoBehaviour
{
    public static EquipPanel CurrentEquipPanel { get; set; }

    public List<Dice> testDices;
    private List<InventoryPanel> panels = new List<InventoryPanel>();
    private List<EquipPanel> equipPanels = new List<EquipPanel>();
    public FollowTarget[] diceObjects = new FollowTarget[100];

    [field:SerializeField]
    public InfoPanel InfoPanel { get; set; }

    public InventoryInput InventoryInput { get; set; }

    [SerializeField] private InventoryPanel samplePanel;

    private void Awake()
    {
        InventoryInput = FindObjectOfType<InventoryInput>();
        equipPanels = new List<EquipPanel>(FindObjectsOfType<EquipPanel>());

        //SetDeck(GameManager.Instance.Deck);

        // 패널 생성
        for (int i = 0; i < testDices.Count; i++)
        {
            InventoryPanel panel = Instantiate(samplePanel, samplePanel.transform.parent);
            panels.Add(panel);

            panel.Init(i, testDices[i]);
        }

        samplePanel.gameObject.SetActive(false);
    }

    private IEnumerator Coroutine()
    {
        yield return null;

        for (int i = 0; i < panels.Count; i++)
        {
            CreateDiceObject(i, testDices[i].DicePrefab);
        }
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
            EquipPanel panel = GetEmptyPanel(deck[i].DiceType);

            if (panel)
            {
                panel.EquipDice(deck[i], i);
            }
        }
    }

    private EquipPanel GetEmptyPanel(DiceType type)
    {
        return equipPanels.Find(x => x.IsEmpty && x.Dice.DiceType == type);
    }
}
