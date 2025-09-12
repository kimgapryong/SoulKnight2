using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    private Dictionary<Type, UnityEngine.Object[]> _uiDic = new Dictionary<Type, UnityEngine.Object[]>();
    private bool _init;
    private void Start()
    {
        Init();
    }
    public virtual bool Init()
    {
        if(!_init)
        {
            _init = true;
            return true;
        }


        return false;
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = System.Enum.GetNames(type);
        UnityEngine.Object[] obj = new UnityEngine.Object[names.Length];
        _uiDic.Add(typeof(T), obj);

        for (int i = 0; i < names.Length; i++)

            obj[i] = gameObject.FindChild<T>(names[i]);
        
    }

    protected void BindImage(Type type) { Bind<Image>(type); }
    protected void BindText(Type type) { Bind<Text>(type); }
    protected void BindObject(Type type) { Bind<GameObject>(type); }
    protected void BindButton(Type type) { Bind<Button>(type); }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs = null;
        if(_uiDic.TryGetValue(typeof(T), out objs) == false)
            return null;

        return objs[idx] as T;
    }
    protected Image GetImage(int idx) {  return Get<Image>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Button GetButton(int idx) {return Get<Button>(idx); }
}
