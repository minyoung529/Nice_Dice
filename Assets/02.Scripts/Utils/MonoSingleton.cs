using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shuttingDown = false;
    private static object locker = new object();
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance " + typeof(T) + "already destroyed. Returning null.");
                return null;
            }

            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    }
                    DontDestroyOnLoad(instance);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        List<T> singles = new List<T>(FindObjectsOfType<T>());

        if (singles.Count > 1)
        {
            Transform[] childs = transform.GetComponentsInChildren<Transform>();
            T original = Instance;

            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].SetParent(original.transform);
            }

            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }

    private void OnDestroy()
    {
        shuttingDown = true;
    }
}