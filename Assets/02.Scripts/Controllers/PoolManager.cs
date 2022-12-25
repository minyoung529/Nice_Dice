using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : ControllerBase
{
    public Transform Root { get; private set; }
    private Dictionary<string, Stack<GameObject>> pools = new Dictionary<string, Stack<GameObject>>();

    public override void OnAwake()
    {
        Root = new GameObject("@Root").transform;
        Object.DontDestroyOnLoad(Root);
    }

    public GameObject Pop(string name)
    {
        if (!pools.ContainsKey(name))
        {
            pools.Add(name, new Stack<GameObject>());
        }

        if (pools[name].Count == 0)
            return null;

        GameObject obj = pools[name].Pop();
        obj.gameObject.SetActive(true);
        obj.transform.SetParent(null);

        return obj;
    }

    public GameObject Pop(GameObject poolObj)
    {
        GameObject obj = Pop(poolObj.name);

        if (obj)
        {
            obj.transform.localScale = Vector3.one;
            return obj;
        }
        else
        {
            /*GameObject*/ obj = Object.Instantiate(poolObj);
            obj.transform.localScale = Vector3.one;
            obj.name = poolObj.name;
            return obj;
        }
    }

    public void Push(GameObject obj)
    {
        //Object.Destroy(obj);
        if (!pools.ContainsKey(obj.name)) return;

        obj.transform.localScale = Vector3.one;

        pools[obj.name].Push(obj);
        obj.transform.SetParent(Root);
        obj.gameObject.SetActive(false);
    }
}
