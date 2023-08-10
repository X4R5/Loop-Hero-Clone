using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.timeScale == 0) return;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse over");
    }
}
