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
    [SerializeField] private CanvasGroup helpPanel;
    [SerializeField] private AudioClip buttonSound;

    [SerializeField] private Transform warningImage;

    private void OnClickButton(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Game:

                if (GameManager.Instance.Deck.Count != 6)
                {
                    warningImage?.gameObject.SetActive(true);
                    warningImage?.DOShakePosition(1f, 20).OnComplete(() => warningImage?.gameObject.SetActive(false));
                }
                else
                {
                    SceneManager.LoadScene(type.ToString());
                }
                break;
            case ButtonType.Lobby:
            case ButtonType.Deck:
                SceneManager.LoadScene(type.ToString());
                break;

            case ButtonType.Exit:
                Application.Quit();
                break;

            case ButtonType.Help:
                helpPanel.gameObject.SetActive(true);
                helpPanel.DOFade(1f, 0.3f);
                break;
        }

        SoundManager.Instance.PlayOneshot(buttonSound);
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
