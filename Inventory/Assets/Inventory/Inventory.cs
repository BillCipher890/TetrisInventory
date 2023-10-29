using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<InventoryModel>().MaybeBecomeRealFields(eventData.pointerDrag.transform);
        GlobalEventManager.SendCellBusyChange();
    }
}
