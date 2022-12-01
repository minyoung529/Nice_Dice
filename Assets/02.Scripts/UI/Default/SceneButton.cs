using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [SerializeField]
    private SceneType sceneType;

    private void Start()
    {
        Button button = GetComponent<Button>();

        if (button)
        {
            button.onClick.AddListener(() => SceneManager.LoadScene((int)sceneType));
        }
    }
}
