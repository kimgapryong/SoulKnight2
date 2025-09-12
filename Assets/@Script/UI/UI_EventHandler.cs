using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    Action clickAction;
    public void SetAction(Action action) {  clickAction = action; } 
    public void OnPointerClick(PointerEventData eventData)
    {
        clickAction?.Invoke();
    }

}
