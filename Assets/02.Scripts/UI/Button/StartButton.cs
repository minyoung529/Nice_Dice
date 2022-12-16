using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using MyBox;

public enum ButtonType
{
    Game, Deck, Help, Lobby, Exit, Length
}

public class StartButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ButtonType type;
    [SerializeField] private bool isSelect = true;

    [ConditionalField(nameof(type), false, ButtonType.Help)]
    [SerializeField]
    private CanvasGroup helpPanel;

    private void OnClickButton(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Game:
                SceneManager.LoadScene("Game");
                break;

            case ButtonType.Lobby:
                SceneManager.LoadScene("Lobby");
                break;

            case ButtonType.Deck:
                SceneManager.LoadScene("Deck");
                break;

            case ButtonType.Exit:
                Application.Quit();
                break;

            case ButtonType.Help:
                helpPanel.gameObject.SetActive(true);
                helpPanel.DOFade(1f, 0.3f);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickButton(type);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.2f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.5f);
    }
}
