using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TMP_Text nextText;
    private TMP_Text defaultText;
    private Color32 color = new Color32(175, 255, 131, 255);

    private void Start()
    {
        defaultText = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        defaultText.color = Color.white;
        nextText.color = color;
    }
}
