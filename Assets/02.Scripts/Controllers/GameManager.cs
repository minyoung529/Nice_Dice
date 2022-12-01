using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Contoller
    private UIManager uiManager = new UIManager();
    public UIManager UI => uiManager;

    // 컨트롤러들(UI, InputManageR)을 모아놓는 곳
    // 한번에 재정의한 함수들을 사용하기 위함
    private List<ControllerBase> controllers = new List<ControllerBase>();
    #endregion

    #region Game
    private bool myTurn = true;
    [SerializeField] private Character player;
    [SerializeField] private Character enemy;

    [SerializeField] private List<Dice> inventory = new List<Dice>();
    [SerializeField] private List<Dice> deck = new List<Dice>();

    public List<Dice> Inventory => inventory;
    public List<Dice> Deck => deck;
    #endregion

    private void Awake()
    {
        // controllers에 컨트롤러들을 넣는다.
        controllers.Add(uiManager);

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
        myTurn = !myTurn;

        if (myTurn)
        {

        }
        else
        {
            enemy.ChangeState(CharacterState.BeforeRoll);
        }
    }
}
