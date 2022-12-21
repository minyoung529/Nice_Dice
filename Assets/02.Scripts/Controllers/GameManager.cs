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

    private CameraMove mainCam;
    #region Property
    public UIManager UI => uiManager;
    public DiceManager Dice
    {
        get
        {
            if (!diceManager)
                diceManager = FindObjectOfType<DiceManager>();
            return diceManager;
        }
    }
    public DataManager Data => dataManager;
    public CameraMove MainCam
    {
        get
        {
            if (!mainCam)
                mainCam = FindObjectOfType<CameraMove>();

            return mainCam;
        }
    }

    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }
    #endregion

    // 컨트롤러들(UI, InputManageR)을 모아놓는 곳
    // 한번에 재정의한 함수들을 사용하기 위함
    private List<ControllerBase> controllers = new List<ControllerBase>();
    #endregion

    #region Game
    public int stage = 1;
   [SerializeField]  private bool playerTurn = false;

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
        set
        {
            enemy = value;
            enemy.Enemy = player;
            player.Enemy = enemy;
        }
        get
        {
            if (!enemy)
                enemy = FindObjectOfType<AIEnemyController>();
            return enemy;
        }
    }

    public List<Dice> Inventory => Data.CurrentUser.inventory;
    public List<Dice> Deck => Data.CurrentUser.deck;

    public static int maxDeal = 0;
    public float DamageWeight { get; set; } = 1;

    public List<string> ClearMonsters { get; set; } = new List<string>();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        // controllers에 컨트롤러들을 넣는다.
        controllers.Add(uiManager);
        controllers.Add(dataManager);

        EventManager.StartListening(Define.ON_RELOAD_GAME, ResetValue);

        // 각 컨틀롤러들이 재정의한 OnAwake를 실행
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

        // 현재는 플레이어의 덱에서만 꺼내온다
        if (playerTurn)
        {
            EventManager.TriggerEvent(Define.ON_START_PLAYER_TURN);
        }
        else
        {
            EventManager.TriggerEvent(Define.ON_START_MONSTER_TURN);
        }
    }

    private void ResetValue()
    {
        maxDeal = 0;
        DamageWeight = 1f;
        stage = 1;
        ClearMonsters = new List<string>();
    }

    private void OnDestroy()
    {
        dataManager.SaveToJson();
        EventManager.StopListening(Define.ON_RELOAD_GAME, ResetValue);
    }
}
