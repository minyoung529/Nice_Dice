using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    #region Contoller
    private UIManager uiManager = new UIManager();
    public UIManager UI => uiManager;

    // ��Ʈ�ѷ���(UI, InputManageR)�� ��Ƴ��� ��
    // �ѹ��� �������� �Լ����� ����ϱ� ����
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
        // controllers�� ��Ʈ�ѷ����� �ִ´�.
        controllers.Add(uiManager);

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
