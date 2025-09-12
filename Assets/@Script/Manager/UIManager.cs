using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 20;
    private Stack<UI_Pop> _stack = new Stack<UI_Pop>();
    public UI_Scene SceneUI {  get; private set; }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public void MakeSubItem<T>(Transform parent = null, string key = null, Action<T> callback = null) where T : UI_Base
    {
        if(string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Fragment/{key}", parent, (go) =>
        {
            T subItem = go.GetOrAddComponent<T>();
            callback?.Invoke(subItem);
        });
    }
    public void ShowSceneUI<T>(string key = null, Action<T> callback = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Scene/{key}", Root.transform, (go) =>
        {
            T scene = go.GetOrAddComponent<T>();
            SceneUI = scene;
            callback?.Invoke(scene);
        });
    }
    public void ShowPopUI<T>(string key = null, Transform parent = null, Action<T> callback = null) where T : UI_Pop
    {
        if(string.IsNullOrEmpty (key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Pop/{key}", null, (go) =>
        {
            T pop = go.GetOrAddComponent<T>();
            _stack.Push(pop);

            if(parent != null)
                go.transform.parent = parent;
            else
                go.transform.parent = Root.transform;

            callback?.Invoke(pop);
        });
    }

    public void ClosePopUI(UI_Pop pop)
    {
        if(_stack.Count == 0)
            return;
        if (_stack.Peek() != pop)
            return;

        ClosePopUI();
    }
    public void ClosePopUI()
    {
        if(_stack.Count == 0) 
            return;
        UI_Pop pop = _stack.Pop();
        UnityEngine.Object.Destroy(pop.gameObject);
        pop = null;
        _order--;
    }
    public void CloseAllPopupUI()
    {
        while (_stack.Count > 0)
            ClosePopUI();
    }
}
