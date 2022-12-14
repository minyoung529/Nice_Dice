using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameManager : MonoSingleton<GameManager>
{
    #region Contoller
    private UIManager uiManager = new UIManager();
    private DataManager dataManager = new DataManager();
    private DiceManager diceManager;
    #region Property
    public UIManager UI => uiManager;
    public DiceManager Dice
    {
        get
        {
            if (!diceManager)
                return FindObjectOfType<DiceManager>();
            return diceManager;
        }
    }
    private DataManager Data => dataManager;

    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }
    #endregion

    // ��Ʈ�ѷ���(UI, InputManageR)�� ��Ƴ��� ��
    // �ѹ��� �������� �Լ����� ����ϱ� ����
    private List<ControllerBase> controllers = new List<ControllerBase>();
    #endregion

    #region Game
    private bool playerTurn = true;

    private Character player;
    private Character enemy;

    public Character Player
    {
        get
        {
            if (player)
                return player;

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            return player;
        }
    }

    public Character Enemy
    {
        get
        {
            if (!enemy)
                enemy = FindObjectOfType<AIEnemyController>();
            return enemy;
        }
    }

    public List<Dice> Inventory => Data.CurrentUser.inventory;
    public List<Dice> Deck => Data.CurrentUser.deck;
    #endregion

    public static int maxDeal = 0;

    private void Awake()
    {
        // controllers�� ��Ʈ�ѷ����� �ִ´�.
        controllers.Add(uiManager);
        controllers.Add(dataManager);

        // �� ��Ʋ�ѷ����� �������� OnAwake�� ����
        foreach (ControllerBase controller in controllers)
        {
            controller.OnAwake();
        }
    }

    private void Start()
    {
        foreach (ControllerBase controller in controllers)
        {
            controller.OnStart();
        }

        //NextTurn();
    }

    private void Update()
    {
        foreach (ControllerBase controller in controllers)
        {
            controller.OnUpdate();
        }
    }

    public void NextTurn()
    {
        playerTurn = !playerTurn;
        //SelectedDices = deck.RandomSelect(Define.DICE_SELECT_COUNT);
        Dice.DiceSelect();

        // ����� �÷��̾��� �������� �����´�
        if (playerTurn)
        {
            EventManager.TriggerEvent(Define.ON_START_PLAYER_TURN);
        }
        else
        {
            EventManager.TriggerEvent(Define.ON_START_MONSTER_TURN);
        }
    }
}
