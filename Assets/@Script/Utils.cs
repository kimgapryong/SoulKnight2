using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        T com = obj.GetComponent<T>();
        if(com == null)
            com = obj.AddComponent<T>();

        return com;
    }
    public static void BindEvent(this GameObject obj, Action callback)
    {
        UI_EventHandler ui_event = obj.AddComponent<UI_EventHandler>();
        ui_event.SetAction(callback);
    }
    public static T FindChild<T>(this GameObject obj, string name) where T : UnityEngine.Object
    {
        if (typeof(T) == typeof(GameObject))
            return obj.FindObject(name) as T;

        foreach (T com in obj.GetComponentsInChildren<T>())
            if (com.name.Equals(name))
                return com;

        return null;
    }

    public static GameObject FindObject(this GameObject obj, string name)
    {
        foreach (Transform trans in obj.GetComponentsInChildren<Transform>())
            if (trans.name.Equals(name))
                return trans.gameObject;

        return null;
    }
}
