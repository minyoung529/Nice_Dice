using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ButtonType
{
    Game, Lobby, Exit, Length
}

public class StartButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ButtonType type;

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

            case ButtonType.Exit:
                Application.Quit();
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickButton(type);
    }
}
