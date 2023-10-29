using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //eventData.pointerDrag.transform.SetParent(eventData.pointerDrag.transform.parent.parent.GetChild(0));
        transform.GetChild(0).GetComponent<InventoryModel>().MaybeBecomeRealFields(eventData.pointerDrag.transform);
        GlobalEventManager.SendCellBusyChange();
    }
}
