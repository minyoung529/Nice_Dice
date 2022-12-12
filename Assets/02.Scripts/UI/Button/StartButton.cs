using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public enum ButtonType
{
    Game, Lobby, Exit, Deck, Length
}

public class StartButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ButtonType type;
    [SerializeField] private bool isSelect = true;
    private void OnClickButton(ButtonType type)
    {
        switch(type)
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
