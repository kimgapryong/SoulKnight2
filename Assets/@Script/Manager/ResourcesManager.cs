using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        UnityEngine.Object obj = null;
        if(_resources.TryGetValue(path, out obj))
            return obj as T;

        obj = Resources.Load($"Prefabs/{path}");   
        _resources.Add(path, obj);

        return obj as T;
    }

    public GameObject Instantiate(string name, Transform trans = null, Action<GameObject> callback = null)
    {
        GameObject obj = Load<GameObject>(name);
        GameObject clone = UnityEngine.Object.Instantiate(obj, trans);
        clone.name = obj.name;

        callback?.Invoke(clone);

        return clone;
    }

    public GameObject Instantiate(string name, Vector3 pos, Quaternion quan, Transform trans = null, Action<GameObject> callback = null)
    {
        GameObject clone = Instantiate(name, trans, callback);
        clone.transform.position = pos;
        clone.transform.rotation = quan;

        return clone;
    }
}
