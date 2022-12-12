using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;
using UnityEngine.SceneManagement;

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
            if(!diceManager)
                return FindObjectOfType<DiceManager>();
            return diceManager;
        }
    }
    private DataManager Data => dataManager;

    public bool PlayerTurn => playerTurn;
    #endregion

    // ��Ʈ�ѷ���(UI, InputManageR)�� ��Ƴ��� ��
    // �ѹ��� �������� �Լ����� ����ϱ� ����
    private List<ControllerBase> controllers = new List<ControllerBase>();
    #endregion

    #region Game
    private bool playerTurn = false;
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;

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
        Debug.Log("NEXT TURN!!!!!!!!!!!!!!!!!");
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
