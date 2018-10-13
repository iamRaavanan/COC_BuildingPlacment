using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Raavanan
{
    public class BtnPointerDown : MonoBehaviour, IPointerDownHandler
    {

        public void OnPointerDown(PointerEventData eventData)
        {
            MainController.instance.SpawnBuilding(this.gameObject.name);
            UIController.instance.DisableScrollUI();
        }
    }
}
