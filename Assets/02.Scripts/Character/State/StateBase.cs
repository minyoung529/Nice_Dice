using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    protected object sendedData;
    public object SendedData => sendedData;

    protected object receiveData;
    protected Character character;

    public StateBase(Character character)
    {
        this.character = character;
    }

    public virtual void OnAwake() { }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnEnd() { }

    public void ReceiveData(object obj) { receiveData = obj; Debug.Log("receive"); }
}
