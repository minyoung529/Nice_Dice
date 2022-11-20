using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChangeState : MonoBehaviour
{
    Character character;

    void Start()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            character.ChangeState(CharacterState.Attack);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            character.ChangeState(CharacterState.Hit);
        }
    }
}
