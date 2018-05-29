using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnPointerDown : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        MainController.instance.SpawnBuilding(this.gameObject.name);
        UIController.instance.DisableScrollUI();
    }
}
