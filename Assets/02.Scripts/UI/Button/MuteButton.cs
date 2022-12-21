using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField]
    private GameObject muteObj;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        muteObj.gameObject.SetActive(false);

        GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        SoundManager.Instance.Mute = !SoundManager.Instance.Mute;
        muteObj.gameObject.SetActive(SoundManager.Instance.Mute);
    }
}
