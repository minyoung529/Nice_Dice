using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeckUIController : MonoBehaviour
{
    public static EquipPanel CurrentEquipPanel { get; set; }

    public List<Dice> testDices;
    private List<InventoryPanel> panels = new List<InventoryPanel>();
    public GameObject[] diceObjects = new GameObject[100];

    [SerializeField] private InventoryPanel samplePanel;

    private void Awake()
    {
        // 패널 생성
        for (int i = 0; i < testDices.Count; i++)
        {
            InventoryPanel panel = Instantiate(samplePanel, samplePanel.transform.parent);
            panels.Add(panel);

            panel.Init(i, testDices[i], null);
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

        diceObjects[index] = Instantiate(prefab, pos, Quaternion.Euler(-22.5f, 45f, -22.5f), null);
        diceObjects[index].transform.localScale *= 12f;
        diceObjects[index].AddComponent<Roll>();

        FollowTarget follow = diceObjects[index].AddComponent<FollowTarget>();
        panels[index].OnChangeActive += follow.ChangeTarget;
        follow.Target = panels[index].transform;
    }
}
