using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEffect : MonoBehaviour
{
    ParticleSystem[] particle;
    ParticleSystem.MainModule main;

    void Start()
    {
        particle = GetComponentsInChildren<ParticleSystem>();

        EventManager.StartListening(Define.ON_NEXT_STAGE, Active);
        EventManager.StartListening(Define.ON_START_PLAYER_TURN, Inactive);
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, Inactive);

        Active();
    }

    private void Active()
    {
        gameObject.SetActive(true);
        foreach (var p in particle)
        {
            p.Play();
        }
    }

    private void Inactive()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_NEXT_STAGE, Active);
        EventManager.StopListening(Define.ON_START_PLAYER_TURN, Inactive);
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, Inactive);
    }
}
